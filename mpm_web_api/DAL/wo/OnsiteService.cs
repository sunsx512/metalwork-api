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
                                           .ToList();
        }

        /// <summary>
        /// 获取可执行的工单信息
        /// </summary>
        /// <param name="virtual_line_id"></param>
        /// <returns></returns>
        public List<wo_config> GetExecutableWo(int virtual_line_id)
        {
            return DB.Queryable<wo_config>().Where(x => x.status == 1)
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
                wmcl.start_time = DateTime.Now;
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
                wml.end_time = DateTime.Now;
                wml.machine_id = curwomachine.machine_id;
                wml.productivity = curwomachine.productivity;
                wml.quantity = curwomachine.quantity;
                wml.standard_time = curwomachine.standard_time;
                wml.start_time = curwomachine.start_time;
                wml.wo_config_id = curwomachine.wo_config_id;
            }
            //完结 当前执行的设备工单日志
            re = DB.Deleteable<wo_machine_cur_log>().Where(x => x.id == curwomachine.id).ExecuteCommand()>0;
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
                vlcl.start_time = DateTime.Now;
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

        public bool FinishWoVirtualLine(int virtual_line_id, int wo_config_id)
        {
            bool re = false;
            virtual_line_log vll = new virtual_line_log();
            //查询当前执行的设备工单日志
            virtual_line_cur_log vlcl = DB.Queryable<virtual_line_cur_log>()
                                                 .Where(x => x.virtual_line_id == virtual_line_id)
                                                 .Where(x => x.wo_config_id == wo_config_id)
                                                 .First();
            if (vlcl != null)
            {
                vll.balance_rate = vlcl.balance_rate;
                vll.end_time = DateTime.Now;
                vll.productivity = vlcl.productivity;
                vll.quantity = vlcl.quantity;
                vll.start_time = vlcl.start_time;
                vll.virtual_line_id = vlcl.virtual_line_id;
                vll.wo_config_id = vlcl.wo_config_id;
            }
            //更新主工单状态
            re = DB.Updateable<wo_config>().Where(x => x.id == wo_config_id).UpdateColumns(it => new wo_config() { status = 3 }).ExecuteCommand() > 0;
            //完结 当前执行的线工单日志
            re = re & DB.Deleteable<virtual_line_cur_log>().Where(x => x.id == vlcl.id).ExecuteCommand()>0;
            //插入到线工单历史记录中
            return re & DB.Insertable<virtual_line_log>(vll).ExecuteCommand()>0;
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
                virtual_line vl = DB.Queryable<virtual_line>().Where(x => x.id == wo.virtual_line_id).First();
                if(vl != null)
                {
                    List<wo_machine> ml = DB.Queryable<wo_machine>().Where(x => x.virtual_line_id == vl.id).ToList();
                    if(ml != null)
                    {
                        //如果是第一台设备 则需要开启线，设备工单 修改主工单状态
                        if (ml.First().machine_id == machine_id)
                        {
                            re = StartWoVirtualLine(vl.id, work_order_id);
                            return  re & StartWoMachine(machine_id, work_order_id);
                        }
                        else
                        {
                            return  StartWoMachine(machine_id, work_order_id);
                        }
                    }
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
                        //如果是第最后一台设备 则需要结束线，设备工单 修改主工单状态
                        if (ml.First().machine_id == machine_id)
                        {
                            re = FinshWoMachine(machine_id);
                            return re & FinishWoVirtualLine(vl.id, work_order_id);
                        }
                        else
                        {
                            return FinishWoVirtualLine(vl.id, work_order_id);
                        }
                    }
                }
            }
            return re;
        }
    }
}
