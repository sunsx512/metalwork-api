using MongoDB.Bson.IO;
using mpm_web_api.Common;
using mpm_web_api.db;
using mpm_web_api.model;
using mpm_web_api.model.m_common;
using mpm_web_api.model.m_wo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JsonConvert = Newtonsoft.Json.JsonConvert;
using SqlSugar;

namespace mpm_web_api.DAL.wo
{
    public class OnsiteService : SqlSugarBase
    {

        AreaPropertyService areaPropertyService = new AreaPropertyService();
        /// <summary>
        /// 获取执行中的设备工单信息
        /// </summary>
        /// <param name="machine_id"></param>
        /// <returns></returns>
        public List<wo_machine_cur_log> GetExecutingWoByMachineLog(int machine_id)
        {
           return DB.Queryable<wo_machine_cur_log>()
                                           .Where(x => x.machine_id == machine_id)
                                           .OrderBy(x=>x.id)
                                           .ToList();
        }

        /// <summary>
        /// 按设备获取可执行的工单信息
        /// </summary>
        /// <param name="machine_id"></param>
        /// <returns></returns>
        public List<wo_config> GetExecutableWo(int machine_id)
        {
            List<wo_machine> wo_machines = DB.Queryable<wo_machine>().Where(x => x.machine_id == machine_id).ToList();
            List<wo_config> wo_configs = new List<wo_config>();
            foreach(wo_machine obj in wo_machines)
            {
                wo_configs.AddRange(DB.Queryable<wo_config>().Where(x => x.status == 1 || x.status == 2 || x.status == 12)
                                            .Where(x => x.virtual_line_id == obj.virtual_line_id)
                                            .ToList());
            }
            return wo_configs;
        }
        /// <summary>
        /// 执行中的工单
        /// </summary>
        /// <param name="virtual_line_id"></param>
        /// <returns></returns>
        public List<wo_config> GetExecutingWo(int virtual_line_id)
        {
            return DB.Queryable<wo_config>().Where(x => x.status == 2)
                                            .Where(x => x.virtual_line_id == virtual_line_id)
                                            .ToList();
        }

        /// <summary>
        /// 开始设备工单
        /// </summary>
        /// <param name="machine_id"></param>
        /// <param name="wo_config_id"></param>
        /// <returns></returns>
        public bool StartWoMachine(int machine_id,int wo_config_id)
        {
            wo_machine_cur_log wmcl = new wo_machine_cur_log();
            wo_config wo = DB.Queryable<wo_config>().Where(x => x.id == wo_config_id).First();
            if(wo != null)
            {
                virtual_line vl = DB.Queryable<virtual_line>().Where(x => x.id == wo.virtual_line_id).First();
                if(vl != null)
                {
                    List<wo_machine> ml = DB.Queryable<wo_machine>().Where(x => x.virtual_line_id == vl.id).ToList();
                    if(ml.Count>0)
                    {
                        //拆分标准用时
                        int i = 0;
                        foreach(var obj in ml)
                        {
                            if(obj.machine_id == machine_id)
                            {
                                wmcl.standard_time = Convert.ToDecimal(wo.standard_time.Split(';')[i]);
                            }
                            i++;
                        }
                    }
                }              
                wmcl.wo_config_id = wo_config_id;
                wmcl.machine_id = machine_id;
                wmcl.standard_num = wo.standard_num;
                wmcl.start_time = DateTime.Now.AddHours(GlobalVar.time_zone);
            }
            return DB.Insertable<wo_machine_cur_log>(wmcl).ExecuteCommand()>0;
        }
        /// <summary>
        /// 结束设备工单
        /// </summary>
        /// <param name="machine_id"></param>
        /// <returns></returns>
        public bool FinshWoMachine(int machine_id)
        {
            bool re = false;
            wo_machine_log wml = new wo_machine_log();
            //查询当前执行的设备工单日志
            wo_machine_cur_log curwomachine = DB.Queryable<wo_machine_cur_log>()
                                                                  .Where(x => x.machine_id == machine_id)
                                                                  .First();
            if (curwomachine != null)
            {
                wml = common.AutoCopy<wo_machine_cur_log, wo_machine_log>(curwomachine);
                wml.end_time = DateTime.Now.AddHours(GlobalVar.time_zone);
                RemoveBreakpoint(curwomachine.wo_config_id);
            }
            //完结 当前执行的设备工单日志
            re = DB.Deleteable<wo_machine_cur_log>(curwomachine.id).ExecuteCommand()>0;
            //插入到设备工单历史记录中
            return re & DB.Insertable<wo_machine_log>(wml).ExecuteCommand()>0;
        }

        /// <summary>
        /// 开始线工单
        /// </summary>
        /// <param name="wo_config_id"></param>
        /// <returns></returns>
        public bool StartWoVirtualLine(int wo_config_id)
        {
            bool re = false;
            virtual_line_cur_log vlcl = new virtual_line_cur_log();
            wo_config wo = DB.Queryable<wo_config>().Where(x => x.id == wo_config_id).First();
            if(wo != null)
            {
                virtual_line_log vll = DB.Queryable<virtual_line_log>()?
                                         .Where(x => x.virtual_line_id == wo.virtual_line_id)
                                         .OrderBy(x=>x.end_time, OrderByType.Desc)
                                         .First();
                //如果存在上一笔工单 则需要计算换线的时间  ？？可能会有问题  时间过长
                if(vll != null)
                {
                    DateTime now = DateTime.Now.AddHours(GlobalVar.time_zone);
                    //如果换线期间有休息时间  需要减去休息时间
                    double rest_time = 0;
                    //获取固定排休时间
                    area_property FixedBreak = areaPropertyService.QueryFixedBreak().Where(x => x.area_node_id == 1).FirstOrDefault();
                    //获取非固定排休时间
                    area_property UnfixedBreak = areaPropertyService.QueryUnfixedBreak().Where(x => x.area_node_id == 1).FirstOrDefault();
                    if(FixedBreak != null )
                    {
                        area_property_break fix = JsonConvert.DeserializeObject<area_property_break>(FixedBreak.format);
                        foreach(rest obj in fix.rest)
                        {
                            DateTime start_rest = DateTime.Parse(obj.start);
                            DateTime end_rest = DateTime.Parse(obj.end);
                            //如果换线时间段内包含休息时间 则需要减去休息时间
                            if(common.IsContainTimeSpan(start_rest, (DateTime)vll.end_time, now))
                            {
                                if (common.IsContainTimeSpan(end_rest, (DateTime)vll.end_time, now))
                                {
                                    rest_time += (end_rest - start_rest).TotalSeconds;
                                }
                            }
                        }
                    }
                    if (UnfixedBreak != null)
                    {
                        area_property_break unfix = JsonConvert.DeserializeObject<area_property_break>(UnfixedBreak.format);
                        foreach (rest obj in unfix.rest)
                        {
                            DateTime start_rest = Convert.ToDateTime(obj.start);
                            DateTime end_rest = Convert.ToDateTime(obj.end);
                            //如果换线时间段内包含休息时间 则需要减去休息时间
                            if (common.IsContainTimeSpan(start_rest, (DateTime)vll.end_time, now))
                            {
                                if (common.IsContainTimeSpan(end_rest, (DateTime)vll.end_time, now))
                                {
                                    rest_time += (end_rest - start_rest).TotalSeconds;
                                }
                            }
                        }
                    }                   
                    TimeSpan ts = now - (DateTime)vll.end_time;
                    decimal change_over = Convert.ToDecimal(ts.TotalSeconds - rest_time);
                    vlcl.change_over = change_over;
                }
                vlcl.start_time = DateTime.Now.AddHours(GlobalVar.time_zone);
                vlcl.virtual_line_id = wo.virtual_line_id;
                vlcl.wo_config_id = wo.id;
                //更新主工单状态
                re = DB.Updateable<wo_config>().Where(x => x.id == wo.id).UpdateColumns(it => new wo_config() { status = 2 }).ExecuteCommand()>0;
                return re & DB.Insertable(vlcl).ExecuteCommand() > 0;
            }
            else
            {
                return false;
            }
        }

        public bool FinishWoVirtualLine(int virtual_line_id, int wo_config_id,int status,int machine_id,decimal count)
        {
            bool re = false;
            virtual_line_log vll = new virtual_line_log();
            //查询当前执行的设备工单日志
            virtual_line_cur_log vlcl = DB.Queryable<virtual_line_cur_log>()
                                                 .Where(x => x.virtual_line_id == virtual_line_id)
                                                 .Where(x => x.wo_config_id == wo_config_id)
                                                 .First();
            wo_config dBWoConfig = DB.Queryable<wo_config>().Where(x => x.id == wo_config_id).First();
            wo_machine_log machinelog = DB.Queryable<wo_machine_log>().Where(x => x.machine_id == machine_id)
                                                                  .Where(x => x.wo_config_id == wo_config_id)
                                                                  .First();
            if (vlcl != null)
            {
                vll = common.AutoCopy<virtual_line_cur_log, virtual_line_log>(vlcl);
                vll.quantity = count;
                vll.end_time = DateTime.Now.AddHours(GlobalVar.time_zone);
            }

            string[] standrd_times = dBWoConfig.standard_time.Split(';');
            //取最后一站的 标准时间
            decimal per_cost = Convert.ToDecimal(standrd_times[standrd_times.Count() - 1]);
            TimeSpan ts = (DateTime)machinelog.end_time - machinelog.start_time;
            decimal cost = Convert.ToDecimal(ts.TotalSeconds);

            //大于标准生产时间  为逾期 工单
            if (cost > dBWoConfig.standard_num * per_cost)
            {
                //插入逾期工单表中
                overdue_work_order woo = new overdue_work_order();
                woo.start_time = vll.start_time;
                woo.end_time = vll.end_time;
                woo.wo_config_id = vll.wo_config_id;
                woo.overdue_time = cost - dBWoConfig.standard_num * per_cost;
                re = DB.Insertable<overdue_work_order>(woo).ExecuteCommand() > 0;
                //更新主工单的状态
                re = re & DB.Updateable<wo_config>().Where(x => x.id == wo_config_id).UpdateColumns(it => new wo_config() { status = status }).ExecuteCommand() > 0;

            }
            else
            {
                //更新主工单的状态
                re = DB.Updateable<wo_config>().Where(x => x.id == wo_config_id).UpdateColumns(it => new wo_config() { status = status }).ExecuteCommand() > 0;
            }
            RemoveBreakpoint(wo_config_id);
            //完结 当前执行的线工单日志
            re = re & DB.Deleteable<virtual_line_cur_log>(vlcl.id).ExecuteCommand()>0;
            //插入到线工单历史记录中
            return re & DB.Insertable(vll).ExecuteCommand()>0;
        }

        public bool FinishWoVirtualLine(int virtual_line_id, int wo_config_id, int status)
        {
            bool re = false;
            virtual_line_log vll = new virtual_line_log();
            //查询当前执行的设备工单日志
            virtual_line_cur_log vlcl = DB.Queryable<virtual_line_cur_log>()
                                                 .Where(x => x.virtual_line_id == virtual_line_id)
                                                 .Where(x => x.wo_config_id == wo_config_id)
                                                 .First();
            wo_config dBWoConfig = DB.Queryable<wo_config>().Where(x => x.id == wo_config_id).First();
            if (vlcl != null)
            {
                vll = common.AutoCopy<virtual_line_cur_log, virtual_line_log>(vlcl);
                vll.end_time = DateTime.Now.AddHours(GlobalVar.time_zone);
            }
            if(dBWoConfig != null)
            {
                re = DB.Updateable<wo_config>().Where(x => x.id == dBWoConfig.id).UpdateColumns(it => new wo_config() { status = status }).ExecuteCommand() > 0;
            }
            //完结 当前执行的线工单日志
            re = re & DB.Deleteable<virtual_line_cur_log>(vlcl.id).ExecuteCommand() > 0;
            //插入到线工单历史记录中
            return re & DB.Insertable(vll).ExecuteCommand() > 0;
        }


        /// <summary>
        /// 开始工单
        /// </summary>
        /// <param name="machine_id"></param>
        /// <param name="work_order_id"></param>
        /// <returns></returns>
        public bool StartWorkOrder(int machine_id ,int work_order_id)
        {
            bool re = false;
            wo_config wo = DB.Queryable<wo_config>().Where(x => x.id == work_order_id).First();
            if(wo != null)
            {
                if(wo.status < 3)
                {
                    virtual_line vl = DB.Queryable<virtual_line>().Where(x => x.id == wo.virtual_line_id).First();
                    if (vl != null)
                    {
                        List<wo_machine> ml = DB.Queryable<wo_machine>().Where(x => x.virtual_line_id == vl.id).ToList();
                        if (ml != null)
                        {
                            wo_machine_log wml = DB.Queryable<wo_machine_log>().Where(x => x.machine_id == machine_id)
                                                   .Where(x => x.wo_config_id == work_order_id).First();
                            //只有没有结束过的设备工单才能再次被开启
                            if(wml == null)
                            {
                                virtual_line_cur_log vlcl = DB.Queryable<virtual_line_cur_log>().Where(x => x.wo_config_id == work_order_id).First();
                                if (vlcl != null)
                                {
                                    return StartWoMachine(machine_id, work_order_id);
                                }
                                //如果没有开启工单 则自动开启线工单
                                else
                                {
                                    re = StartWoVirtualLine(work_order_id);
                                    return re & StartWoMachine(machine_id, work_order_id);
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
                //未结工单
                else if (wo.status == 12)
                {
                    return StartSuspendWorkOrder(work_order_id);
                }
            }
            return re;
        }
        /// <summary>
        /// 结束工单
        /// </summary>
        /// <param name="machine_id"></param>
        /// <param name="work_order_id"></param>
        /// <returns></returns>
        public bool FinishWorkOrder(int machine_id, int work_order_id)
        {
            bool re = false;
            wo_config wo = DB.Queryable<wo_config>().Where(x => x.id == work_order_id).First();
            if (wo != null)
            {
                virtual_line vl = DB.Queryable<virtual_line>().Where(x => x.id == wo.virtual_line_id).First();
                if (vl != null)
                {
                    List<wo_machine> ml = DB.Queryable<wo_machine>().Where(x => x.virtual_line_id == vl.id).ToList();
                    if (ml != null)
                    {
                        //判断该虚拟线下的其他设备是否已经完结
                        bool OtherMachinesFinshed = true;                        
                        foreach(wo_machine wm in ml.Where(x=>x.machine_id != machine_id))
                        {
                            wo_machine_log wml = DB.Queryable<wo_machine_log>().Where(x => x.machine_id == wm.machine_id)
                                                                               .Where(x=>x.wo_config_id == work_order_id).First();
                            if(wml == null)
                            {
                                OtherMachinesFinshed = false;
                                break;
                            }
                        }                      
                        //如果其他站位已经完结了 则完结当前站位 及整个虚拟线
                        if (OtherMachinesFinshed)
                        {
                            CalMachineProductivity(machine_id);
                            re = FinshWoMachine(machine_id);
                            //计算线生产力及生产效率
                            CalVirtualLineProductivity(work_order_id);
                            decimal count = DB.Queryable<wo_machine_log>().Where(x => x.machine_id == machine_id)
                                                                          .Where(x => x.wo_config_id == work_order_id).First().quantity;
                            return re & FinishWoVirtualLine(vl.id, work_order_id,3, machine_id, count);
                        }
                        //如果没有完结  则完结当前站位
                        else
                        {
                            return FinshWoMachine(machine_id);
                        }
                    }
                }
            }
            return re;
        }
        /// <summary>
        /// 未结工单
        /// </summary>
        /// <param name="work_order_id"></param>
        /// <returns></returns>
        public bool SuspendWorkOrder(int work_order_id)
        {
            bool re = false;
            wo_config wo = DB.Queryable<wo_config>().Where(x => x.id == work_order_id).First();
            if (wo != null)
            {
                virtual_line vl = DB.Queryable<virtual_line>().Where(x => x.id == wo.virtual_line_id).First();
                if (vl != null)
                {
                    List<wo_machine> ml = DB.Queryable<wo_machine>().Where(x => x.virtual_line_id == vl.id).ToList();
                    if (ml != null)
                    {
                        //标记主工单号 状态为12  未结工单
                        re = FinishWoVirtualLine(vl.id, work_order_id,12);
                        List<wo_machine_cur_log> wmls = DB.Queryable<wo_machine_cur_log>()
                            .Where(x => x.wo_config_id == work_order_id).ToList();
                        foreach (wo_machine obj in ml)
                        {
                            //只有有正在执行的工单信息的设备才会被结束
                            wo_machine_cur_log wml = wmls.Where(x => x.machine_id == obj.machine_id).FirstOrDefault();
                            if(wml != null)
                            {
                                re = re & FinshWoMachine(obj.machine_id);
                            }
                        }
                    }
                }
            }
            return re;
        }
        /// <summary>
        /// 开始未结工单
        /// </summary>
        /// <param name="work_order_id"></param>
        /// <returns></returns>
        public bool StartSuspendWorkOrder(int work_order_id)
        {
            bool re = false;
            wo_config wo = DB.Queryable<wo_config>().Where(x => x.id == work_order_id).First();
            if (wo != null)
            {
                //第一步 结转线工单
                virtual_line_log vll = DB.Queryable<virtual_line_log>().Where(x => x.wo_config_id == work_order_id).First();
                if(vll != null)
                {
                    virtual_line_cur_log vlcl = new virtual_line_cur_log();
                    vlcl.bad_quantity = vll.bad_quantity;
                    vlcl.balance_rate = vll.balance_rate;
                    vlcl.productivity = vll.productivity;
                    vlcl.quantity = vll.quantity;
                    vlcl.start_time = vll.start_time;
                    vlcl.virtual_line_id = vll.virtual_line_id;
                    vlcl.wo_config_id = vll.wo_config_id;
                    re = DB.Insertable(vlcl).ExecuteCommand()>0;
                    //删除历史工单记录
                    re = re & DB.Deleteable<virtual_line_log>(vll.id).ExecuteCommand() > 0;
                }
                virtual_line vl = DB.Queryable<virtual_line>().Where(x => x.id == wo.virtual_line_id).First();
                if (vl != null)
                {
                    List<wo_machine> ml = DB.Queryable<wo_machine>().Where(x => x.virtual_line_id == vl.id).ToList();
                    if (ml != null)
                    {
                        List<wo_machine_log> wmls = DB.Queryable<wo_machine_log>()
                                                    .Where(x => x.wo_config_id == work_order_id).ToList();
                        foreach(wo_machine ms in ml)
                        {
                            wo_machine_log obj = wmls.Where(x => x.machine_id == ms.machine_id).FirstOrDefault();
                            if(obj != null)
                            {
                                //第二步 结转设备工单
                                //只有有历史工单信息的才会被结转
                                //只有未完成的 才会转移log
                                if (obj.bad_quantity < obj.standard_num)
                                {
                                    wo_machine_cur_log wmcl = new wo_machine_cur_log();
                                    wmcl.achieving_rate = obj.achieving_rate;
                                    wmcl.bad_quantity = obj.bad_quantity;
                                    wmcl.cycle_time = obj.cycle_time;
                                    wmcl.cycle_time_average = obj.cycle_time_average;
                                    wmcl.start_time = vll.start_time;
                                    wmcl.machine_id = obj.machine_id;
                                    wmcl.productivity = obj.productivity;
                                    wmcl.quantity = obj.quantity;
                                    wmcl.standard_num = obj.standard_num;
                                    wmcl.standard_time = obj.standard_time;
                                    wmcl.wo_config_id = obj.wo_config_id;
                                    re = re & DB.Insertable<wo_machine_cur_log>(wmcl).ExecuteCommand() > 0;
                                    //删除历史工单记录
                                    re = re & DB.Deleteable<wo_machine_log>(obj.id).ExecuteCommand() > 0;
                                }
                            }
                        }
                    }
                }
            }
            if(re)
            {
                //最后更新工单状态
                re = DB.Updateable<wo_config>().Where(x => x.id == work_order_id).UpdateColumns(it => new wo_config() { status = 2 }).ExecuteCommand() > 0;
            }
            return re;
        }

        /// <summary>
        /// 修改工单实际的修改数量
        /// </summary>
        /// <param name="machine_id"></param>
        /// <param name="wo_config_id"></param>
        /// <returns></returns>
        public bool modifyCount(int machine_id, int wo_config_id,decimal count)
        {
            wo_config wo = DB.Queryable<wo_config>().Where(x => x.id == wo_config_id).First();
            wo_machine_cur_log wmcl = DB.Queryable<wo_machine_cur_log>()
                                         .Where(x => x.machine_id == machine_id || x.wo_config_id == machine_id).First();
            if(wmcl != null)
            {
                return DB.Updateable<wo_machine_cur_log>().Where(x=>x.machine_id == machine_id).UpdateColumns(it => new wo_machine_cur_log() { quantity = count }).ExecuteCommand() > 0;
            }
            return false;
        }

        public void CalVirtualLineProductivity(int workorder_id)
        {
            List<wo_machine_cur_log> wcls = DB.Queryable<wo_machine_cur_log>().ToList();
            virtual_line_cur_log vlcl = DB.Queryable<virtual_line_cur_log>().Where(x=>x.wo_config_id == workorder_id)?.First();
            List<machine> machines = DB.Queryable<machine>().ToList();
            //获取工单信息
            wo_config wo = DB.Queryable<wo_config>().Where(x => x.id == workorder_id)?.First();
            //获取虚拟线信息
            List<wo_machine> wm = DB.Queryable<wo_machine>().Where(x => x.virtual_line_id == wo.virtual_line_id).ToList();
            double vl_production_time = 0;   //虚拟线的生产时间累计  各台设备的生产时间累加
            decimal vl_standard_production_time = 0;   //标准生产时间 实际数量*标准时间
            decimal vl_standard_quantity = 0; //线标准数量
            decimal vl_achievement_rate = 0;  //达成率           
            decimal vl_productivity = 0;  //生产力
            decimal vl_production_efficiency = 0; //生产效率
            double vl_rest_time = 0; //累计的休息时间
            double vl_error_time = 0;  //累计的异常时间  暂定品质异常和设备异常时间
            //第三步 查看是否在休息时间内   如果在的话 不计算 不更新
            area_property FixedBreak = DB.Queryable<area_property>().Where(x => x.name_en == "fixed_break" && x.area_node_id == 1).First();
            //获取非固定排休时间
            area_property UnfixedBreak = DB.Queryable<area_property>().Where(x => x.name_en == "unfixed_break" && x.area_node_id == 1).First();
            //计算设备生产力及生产效率
            foreach (wo_machine mc in wm)
            {
                DateTime now = DateTime.Now.AddHours(GlobalVar.time_zone);
                wo_machine_cur_log wcl = wcls.Where(x => x.machine_id == mc.machine_id).FirstOrDefault();
                if (wcl == null)
                {
                    //查询历史记录
                    wo_machine_log wl = DB.Queryable<wo_machine_log>().Where(x => x.wo_config_id == workorder_id)
                                                                      .Where(x => x.machine_id == mc.machine_id).First();
                    if (wl != null)
                    {
                        wcl = wl;
                        now = wl.end_time;
                    }
                }
                if (wcl != null)
                {
                    //标记是否是休息时间
                    bool is_rest = false;
                    //标记是否是异常时间
                    bool is_error = false;
                    //达成率
                    decimal achievement_rate = 0;
                    //生产力
                    decimal productivity = 0;
                    //生产效率
                    decimal production_efficiency = 0;
                    //累计的休息时间
                    double rest_time = 0;
                    //累计的异常时间  暂定品质异常和设备异常时间
                    double error_time = 0;
                    //计算达成率
                    achievement_rate = wcl.quantity / wcl.standard_num;
                    //计算生产效率
                    //第一步 计算生产时间

                    //第二步 计算当前生产的总时间
                    double ts_difference = (now - wcl.start_time).TotalSeconds;
                    //固定排休
                    if (FixedBreak != null)
                    {
                        area_property_break fix = JsonConvert.DeserializeObject<area_property_break>(FixedBreak.format);
                        foreach (rest obj in fix.rest)
                        {
                            DateTime start_rest = DateTime.Parse(obj.start);
                            DateTime end_rest = DateTime.Parse(obj.end);
                            //如果在休息时间内 则不计算 不更新
                            if (common.IsContainTimeSpan(now, start_rest, end_rest))
                            {
                                is_rest = true;
                                break;
                            }
                            //如果 运行时间中有休息时间 则累计休息时间
                            if (common.IsContainTimeSpan(start_rest, wcl.start_time, now))
                            {
                                if (common.IsContainTimeSpan(end_rest, wcl.start_time, now))
                                {
                                    rest_time += (end_rest - start_rest).TotalSeconds;
                                }
                            }
                        }
                    }
                    //非固定排休
                    if (UnfixedBreak != null)
                    {
                        area_property_break unfix = JsonConvert.DeserializeObject<area_property_break>(UnfixedBreak.format);
                        foreach (rest obj in unfix.rest)
                        {
                            DateTime start_rest = Convert.ToDateTime(obj.start);
                            DateTime end_rest = Convert.ToDateTime(obj.end);
                            //如果在休息时间内 则不计算 不更新
                            if (common.IsContainTimeSpan(now, start_rest, end_rest))
                            {
                                is_rest = true;
                                rest_time += (now - start_rest).TotalSeconds;
                                break;
                            }

                            //如果 运行时间中有休息时间 则累计休息时间
                            if (common.IsContainTimeSpan(start_rest, wcl.start_time, now))
                            {
                                if (common.IsContainTimeSpan(end_rest, wcl.start_time, now))
                                {
                                    rest_time += (end_rest - start_rest).TotalSeconds;
                                }
                            }
                        }
                    }
                    //第四步 查看是否有异常时间 
                    machine machine = machines.Where(x => x.id == mc.machine_id).FirstOrDefault();
                    if (machine != null)
                    {
                        List<error_log> els = DB.Queryable<error_log>()
                                                .Where(x => x.machine_name == machine.name_en && x.start_time > wcl.start_time)
                                                .ToList();
                        //如果存在未解除的异常 则直接跳过 不计算
                        if (els.Where(x => x.status == 0 && x.status == 1).ToList().Count > 0)
                        {
                            is_error = true;
                        }
                        //没有未解除的异常
                        else
                        {
                            //查询已解除的异常
                            List<error_log> fels = els.Where(x => x.status == 2).ToList();
                            if (fels != null)
                            {
                                foreach (error_log el in fels)
                                {
                                    if (common.IsContainTimeSpan((DateTime)el.start_time, wcl.start_time, now))
                                    {
                                        if (common.IsContainTimeSpan((DateTime)el.release_time, wcl.start_time, now))
                                        {
                                            error_time += ((DateTime)el.release_time - (DateTime)el.start_time).TotalSeconds;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    vl_rest_time += rest_time;
                    vl_error_time += error_time;
                    vl_production_time += ts_difference;
                    //除去休息时间和异常时间的 工作时间 生产力
                    double uptime1 = ts_difference - rest_time - error_time;
                    //除去休息时间和异常时间的 工作时间 生产效率
                    double uptime2 = ts_difference - rest_time;
                    vl_standard_production_time += wcl.standard_time * wcl.quantity;
                    productivity = wcl.standard_time * wcl.quantity / (decimal)uptime1;
                    production_efficiency = wcl.standard_time * wcl.quantity / (decimal)uptime2;
                }
            }
            //除去休息时间和异常时间的 工作时间 生产力
            double vl_uptime1 = vl_production_time - vl_rest_time - vl_error_time;
            //除去休息时间和异常时间的 工作时间 生产效率
            double vl_uptime2 = vl_production_time - vl_rest_time;
            vl_productivity = vl_standard_production_time / (decimal)vl_uptime1;
            vl_production_efficiency = vl_standard_production_time / (decimal)vl_uptime2;
            DB.Updateable<virtual_line_cur_log>().UpdateColumns(it => new virtual_line_cur_log() { productivity = vl_productivity, production_efficiency = vl_production_efficiency })
                             .Where(x => x.wo_config_id == workorder_id).ExecuteCommand(); 
        }


        /// <summary>
        /// 计算设备生产率
        /// </summary>
        /// <param name="wcl"></param>
        public void CalMachineProductivity(int machine_id)
        {
            wo_machine_cur_log wcl = DB.Queryable<wo_machine_cur_log>()
                                                      .Where(x => x.machine_id == machine_id)
                                                      .First();
            if (wcl != null)
            {
                //标记是否是休息时间
                bool is_rest = false;
                //标记是否是异常时间
                bool is_error = false;
                //达成率
                decimal achievement_rate = 0;
                //生产力
                decimal productivity = 0;
                //生产效率
                decimal production_efficiency = 0;
                //累计的休息时间
                double rest_time = 0;
                //累计的异常时间  暂定品质异常和设备异常时间
                double error_time = 0;
                //计算达成率
                achievement_rate = wcl.quantity / wcl.standard_num;
                //计算生产效率
                //第一步 计算生产时间
                DateTime now = DateTime.Now.AddHours(GlobalVar.time_zone);
                //第二步 计算当前生产的总时间
                double ts_difference = (now - wcl.start_time).TotalSeconds;
                //第三步 查看是否在休息时间内   如果在的话 不计算 不更新
                area_property FixedBreak = DB.Queryable<area_property>().Where(x => x.name_en == "fixed_break" && x.area_node_id == 1).First();
                //获取非固定排休时间
                area_property UnfixedBreak = DB.Queryable<area_property>().Where(x => x.name_en == "unfixed_break" && x.area_node_id == 1).First();
                //固定排休
                if (FixedBreak != null)
                {
                    area_property_break fix = JsonConvert.DeserializeObject<area_property_break>(FixedBreak.format);
                    foreach (rest obj in fix.rest)
                    {
                        DateTime start_rest = DateTime.Parse(obj.start);
                        DateTime end_rest = DateTime.Parse(obj.end);
                        //如果在休息时间内 则不计算 不更新
                        if (common.IsContainTimeSpan(now, start_rest, end_rest))
                        {
                            is_rest = true;
                            break;
                        }
                        //如果 运行时间中有休息时间 则累计休息时间
                        if (common.IsContainTimeSpan(start_rest, wcl.start_time, now))
                        {
                            if (common.IsContainTimeSpan(end_rest, wcl.start_time, now))
                            {
                                rest_time += (end_rest - start_rest).TotalSeconds;
                            }
                        }
                    }
                }
                //非固定排休
                if (UnfixedBreak != null)
                {
                    area_property_break unfix = JsonConvert.DeserializeObject<area_property_break>(UnfixedBreak.format);
                    foreach (rest obj in unfix.rest)
                    {
                        DateTime start_rest = Convert.ToDateTime(obj.start);
                        DateTime end_rest = Convert.ToDateTime(obj.end);
                        //如果在休息时间内 则不计算 不更新
                        if (common.IsContainTimeSpan(now, start_rest, end_rest))
                        {
                            is_rest = true;
                            rest_time += (now - start_rest).TotalSeconds;
                            break;
                        }
                        //如果 运行时间中有休息时间 则累计休息时间
                        if (common.IsContainTimeSpan(start_rest, wcl.start_time, now))
                        {
                            if (common.IsContainTimeSpan(end_rest, wcl.start_time, now))
                            {
                                rest_time += (end_rest - start_rest).TotalSeconds;
                            }
                        }
                    }
                }
                //第四步 查看是否有异常时间 
                machine machine = DB.Queryable<machine>().Where(x => x.id == wcl.machine_id)?.First();
                if (machine != null)
                {
                    List<error_log> els = DB.Queryable<error_log>()
                                            .Where(x => x.machine_name == machine.name_en && x.start_time > wcl.start_time)
                                            .ToList();
                    //如果存在未解除的异常 则直接跳过 不计算
                    if (els.Where(x => x.status == 0 && x.status == 1).ToList().Count > 0)
                    {
                        is_error = true;
                    }
                    //没有未解除的异常
                    else
                    {
                        //查询已解除的异常
                        List<error_log> fels = els.Where(x => x.status == 2).ToList();
                        if (fels != null)
                        {
                            foreach (error_log el in fels)
                            {
                                if (common.IsContainTimeSpan((DateTime)el.start_time, wcl.start_time, now))
                                {
                                    if (common.IsContainTimeSpan((DateTime)el.release_time, wcl.start_time, now))
                                    {
                                        error_time += ((DateTime)el.release_time - (DateTime)el.start_time).TotalSeconds;
                                    }
                                }
                            }
                        }
                    }
                }
                //除去休息时间和异常时间的 工作时间 生产力
                double uptime1 = ts_difference - rest_time - error_time;
                //除去休息时间和异常时间的 工作时间 生产效率
                double uptime2 = ts_difference - rest_time;
                productivity = wcl.standard_time * wcl.quantity / (decimal)uptime1;
                production_efficiency = wcl.standard_time * wcl.quantity / (decimal)uptime2;
                //如果是休息状态 则不会计算 
                if (!is_rest)
                {
                    //更新生产力及生产效率
                    if (!is_error)
                    {
                        DB.Updateable<wo_machine_cur_log>().UpdateColumns(it => new wo_machine_cur_log() { productivity = productivity, production_efficiency = production_efficiency, achieving_rate = wcl.quantity / wcl.standard_num })
                                           .Where(x => x.machine_id == wcl.machine_id).ExecuteCommand();
                    }
                    //只更新生产力
                    else
                    {
                        DB.Updateable<wo_machine_cur_log>().UpdateColumns(it => new wo_machine_cur_log() { productivity = productivity, achieving_rate = wcl.quantity / wcl.standard_num })
                                             .Where(x => x.machine_id == wcl.machine_id).ExecuteCommand();
                    }
                }
            }
        }

        public bool RemoveBreakpoint(int workorder_id)
        {
            try
            {
                return DB.Deleteable<break_point_log>(x => x.work_order_id == workorder_id).ExecuteCommand() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}
