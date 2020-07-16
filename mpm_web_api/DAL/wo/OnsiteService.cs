using mpm_web_api.Common;
using mpm_web_api.db;
using mpm_web_api.model;
using mpm_web_api.model.m_wo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL.wo
{
    public class OnsiteService : SqlSugarBase
    {
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
                wml.achieving_rate = curwomachine.achieving_rate;
                wml.bad_quantity = curwomachine.bad_quantity;
                wml.cycle_time = curwomachine.cycle_time;
                wml.cycle_time_average = curwomachine.cycle_time_average;
                wml.end_time = DateTime.Now.AddHours(GlobalVar.time_zone);
                wml.machine_id = curwomachine.machine_id;
                wml.productivity = curwomachine.productivity;
                wml.quantity = curwomachine.quantity;
                wml.standard_time = curwomachine.standard_time;
                wml.start_time = curwomachine.start_time;
                wml.wo_config_id = curwomachine.wo_config_id;
                wml.standard_num = curwomachine.standard_num;
            }
            //完结 当前执行的设备工单日志
            re = DB.Deleteable<wo_machine_cur_log>(curwomachine.id).ExecuteCommand()>0;
            //插入到设备工单历史记录中
            return re & DB.Insertable<wo_machine_log>(wml).ExecuteCommand()>0;
        }

        /// <summary>
        /// 开始线工单
        /// </summary>
        /// <param name="virtual_line_id"></param>
        /// <param name="wo_config_id"></param>
        /// <returns></returns>
        public bool StartWoVirtualLine(int virtual_line_id, int wo_config_id)
        {
            bool re = false;
            virtual_line_cur_log vlcl = new virtual_line_cur_log();
            wo_config wo = DB.Queryable<wo_config>().Where(x => x.id == wo_config_id).First();
            if(wo != null)
            {
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

        public bool FinishWoVirtualLine(int virtual_line_id, int wo_config_id,int status,int machine_id)
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
                vll.balance_rate = vlcl.balance_rate;
                vll.end_time = DateTime.Now.AddHours(GlobalVar.time_zone);
                vll.productivity = vlcl.productivity;
                vll.quantity = vlcl.quantity;
                vll.start_time = vlcl.start_time;
                vll.virtual_line_id = vlcl.virtual_line_id;
                vll.wo_config_id = vlcl.wo_config_id;
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
                vll.balance_rate = vlcl.balance_rate;
                vll.end_time = DateTime.Now.AddHours(GlobalVar.time_zone);
                vll.productivity = vlcl.productivity;
                vll.quantity = vlcl.quantity;
                vll.start_time = vlcl.start_time;
                vll.virtual_line_id = vlcl.virtual_line_id;
                vll.wo_config_id = vlcl.wo_config_id;
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
                                    re = StartWoVirtualLine(vl.id, work_order_id);
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
                            re = FinshWoMachine(machine_id);
                            return re & FinishWoVirtualLine(vl.id, work_order_id,3, machine_id);
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


    }
}
