using MongoDB.Bson;
using mpm_web_api.db;
using mpm_web_api.DB;
using mpm_web_api.model;
using mpm_web_api.model.m_wo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL.andon
{
    public class ErrorOnsiteService: SqlSugarBase
    {
        public bool QualityTrigger(int machine_id)
        {
            //查询绑定的异常触发按钮
            tag_type_sub tag_Type_Sub = DB.Queryable<tag_type_sub>().Where(x => x.name_en == "quality_error").First();
            if(tag_Type_Sub != null)
            {
                if(AddLog(machine_id, tag_Type_Sub))
                {

                    tag_info tag_Info = DB.Queryable<tag_info>()
                                                   .Where(x => x.machine_id == machine_id)
                                                   .Where(x => x.tag_type_sub_id == tag_Type_Sub.id).First();
                    //如果有该按钮则需要
                    if(tag_Info != null)
                    {
                        string s = tag_Info.name.Split(':')[0];
                        string t = tag_Info.name.Split(':')[1];
                        SendMGMsg(s, t, 1);
                    }
                    else
                    {
                        string s = "quality_error";
                        machine mc = DB.Queryable<machine>().Where(x => x.id == machine_id).First();
                        string t = mc.name_en;
                        SendMGMsg(s, t, 1);
                    }
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 设备异常触发
        /// </summary>
        /// <param name="machine_id"></param>
        /// <returns></returns>
        public bool EquipmentErrorTrigger(int machine_id)
        {
            //查询绑定的异常触发按钮
            tag_type_sub tag_Type_Sub = DB.Queryable<tag_type_sub>().Where(x => x.name_en == "equipment_error").First();
            if (tag_Type_Sub != null)
            {
                if(AddLog(machine_id, tag_Type_Sub))
                {
                    tag_info tag_Info = DB.Queryable<tag_info>()
                                                   .Where(x => x.machine_id == machine_id)
                                                   .Where(x => x.tag_type_sub_id == tag_Type_Sub.id).First();
                    //如果有该按钮则需要
                    if (tag_Info != null)
                    {
                        string s = tag_Info.name.Split(':')[0];
                        string t = tag_Info.name.Split(':')[1];
                        SendMGMsg(s, t, 1);
                    }
                    else
                    {
                        string s = "equipment_error";
                        machine mc = DB.Queryable<machine>().Where(x => x.id == machine_id).First();
                        string t = mc.name_en;
                        SendMGMsg(s, t, 1);
                    }
                    return true;
                }
            }
            return false;
        }

        public bool MaterialRequestTrigger(int machine_id ,int count,string material_code)
        {
            //查询绑定的物料呼叫按钮
            tag_type_sub tag_Type_Sub = DB.Queryable<tag_type_sub>().Where(x => x.name_en == "material_require").First();
            if (tag_Type_Sub != null)
            {
                if(MaterialRequestLog(machine_id,count,material_code, tag_Type_Sub))
                {
                    tag_info tag_Info = DB.Queryable<tag_info>()
                                                   .Where(x => x.machine_id == machine_id)
                                                   .Where(x => x.tag_type_sub_id == tag_Type_Sub.id).First();
                    //如果有该按钮则需要
                    if (tag_Info != null)
                    {
                        string s = tag_Info.name.Split(':')[0];
                        string t = tag_Info.name.Split(':')[1];
                        SendMGMsg(s, t, 1);
                    }
                    else
                    {
                        string s = "material_require";
                        machine mc = DB.Queryable<machine>().Where(x => x.id == machine_id).First();
                        string t = mc.name_en;
                        SendMGMsg(s, t, 1);
                    }
                    return true;
                }
                
            }
            return false;
        }

        public bool QualityConfirm(int machine_id, int log_id,string person_id)
        {
            //查询绑定的异常签到按钮
            tag_type_sub tag_Type_Sub = DB.Queryable<tag_type_sub>().Where(x => x.name_en == "quality_sign_in").First();
            if (tag_Type_Sub != null)
            {
                if(Confirm(log_id, person_id))
                {
                    tag_info tag_Info = DB.Queryable<tag_info>()
                                                    .Where(x => x.machine_id == machine_id)
                                                    .Where(x => x.tag_type_sub_id == tag_Type_Sub.id).First();
                    //如果有该按钮则需要
                    if (tag_Info != null)
                    {
                        string s = tag_Info.name.Split(':')[0];
                        string t = tag_Info.name.Split(':')[1];
                        SendMGMsg(s, t, 1);
                    }
                    else
                    {
                        string s = "quality_sign_in";
                        machine mc = DB.Queryable<machine>().Where(x => x.id == machine_id).First();
                        string t = mc.name_en;
                        SendMGMsg(s, t, 1);
                    }
                    return true ;
                }
                
            }
            return false;
            
        }

        public bool EquipmentConfirm(int machine_id, int log_id, string person_id)
        {
            //查询绑定的异常签到按钮
            tag_type_sub tag_Type_Sub = DB.Queryable<tag_type_sub>().Where(x => x.name_en == "equipment_sign_in").First();
            if (tag_Type_Sub != null)
            {
                if(Confirm(log_id, person_id))
                {
                    tag_info tag_Info = DB.Queryable<tag_info>()
                                                .Where(x => x.machine_id == machine_id)
                                                .Where(x => x.tag_type_sub_id == tag_Type_Sub.id).First();
                    //如果有该按钮则需要
                    if (tag_Info != null)
                    {
                        string s = tag_Info.name.Split(':')[0];
                        string t = tag_Info.name.Split(':')[1];
                        SendMGMsg(s, t, 1);
                    }
                    else
                    {
                        string s = "equipment_sign_in";
                        machine mc = DB.Queryable<machine>().Where(x => x.id == machine_id).First();
                        string t = mc.name_en;
                        SendMGMsg(s, t, 1);
                    }
                    return true;
                }
            }
            return false;

        }

        public bool Qualityrelease(int machine_id,int log_id,decimal count)
        {
            //查询绑定的异常解除按钮
            tag_type_sub tag_Type_Sub = DB.Queryable<tag_type_sub>().Where(x => x.name_en == "quality_release").First();
            if (tag_Type_Sub != null)
            {
                if(QualityErrorRelease(log_id, count))
                {
                    tag_info tag_Info = DB.Queryable<tag_info>()
                                                .Where(x => x.machine_id == machine_id)
                                                .Where(x => x.tag_type_sub_id == tag_Type_Sub.id).First();
                    //如果有该按钮则需要
                    if (tag_Info != null)
                    {
                        string s = tag_Info.name.Split(':')[0];
                        string t = tag_Info.name.Split(':')[1];
                        SendMGMsg(s, t, 1);
                    }
                    else
                    {
                        string s = "quality_release";
                        machine mc = DB.Queryable<machine>().Where(x => x.id == machine_id).First();
                        string t = mc.name_en;
                        SendMGMsg(s, t, 1);
                    }
                    return true;
                }
            }
            return false;
        }
        public bool EquipmentRelease(int machine_id,int log_id, int error_type_id, int error_type_detail_id)
        {
            //查询绑定的异常解除按钮
            tag_type_sub tag_Type_Sub = DB.Queryable<tag_type_sub>().Where(x => x.name_en == "equipment_release").First();
            if (tag_Type_Sub != null)
            {
                if(EquipmentErrorRelease(log_id, error_type_id, error_type_detail_id))
                {
                     tag_info tag_Info = DB.Queryable<tag_info>()
                                                .Where(x => x.machine_id == machine_id)
                                                .Where(x => x.tag_type_sub_id == tag_Type_Sub.id).First();
                    //如果有该按钮则需要
                    if (tag_Info != null)
                    {
                        string s = tag_Info.name.Split(':')[0];
                        string t = tag_Info.name.Split(':')[1];
                        SendMGMsg(s, t, 1);
                    }
                    else
                    {
                        string s = "equipment_release";
                        machine mc = DB.Queryable<machine>().Where(x => x.id == machine_id).First();
                        string t = mc.name_en;
                        SendMGMsg(s, t, 1);
                    }
                    return true;
                }
            }
            return false;          
        }

        public bool MaterialRequestRelease(int machine_id, int log_id, string description)
        {
            //查询绑定的物料解除按钮
            tag_type_sub tag_Type_Sub = DB.Queryable<tag_type_sub>().Where(x => x.name_en == "material_release").First();
            if (tag_Type_Sub != null)
            {
                if(UpdateMaterialLog(machine_id, log_id, description))
                {
                     tag_info tag_Info = DB.Queryable<tag_info>()
                                                .Where(x => x.machine_id == machine_id)
                                                .Where(x => x.tag_type_sub_id == tag_Type_Sub.id).First();
                    //如果有该按钮则需要
                    if (tag_Info != null)
                    {
                        string s = tag_Info.name.Split(':')[0];
                        string t = tag_Info.name.Split(':')[1];
                        SendMGMsg(s, t, 1);
                    }
                    else
                    {
                        string s = "material_release";
                        machine mc = DB.Queryable<machine>().Where(x => x.id == machine_id).First();
                        string t = mc.name_en;
                        SendMGMsg(s, t, 1);
                    }
                     return true;
                }
            }
            return false;
        }


        private bool AddLog(int machine_id,tag_type_sub ts)
        {
            //查看是否有配置异常配置 理论上最多只会有一条数据
            error_config ec = DB.Queryable<error_config>()
                                                    .Where(x => x.machine_id == machine_id)
                                                    .Where(x => x.tag_type_sub_id == ts.id)
                                                    .First();
            //如果已配置
            if(ec != null)
            {
                //查询设备设备名
                machine mc = DB.Queryable<machine>()
                                    .Where(x => x.id == machine_id).First();
                //如果设备存在
                if(mc != null)
                {
                    List<error_log> error_logs = DB.Queryable<error_log>()
                                                    .Where(x => x.tag_type_sub_name == ts.name_en)
                                                    .Where(x=>x.machine_name ==mc.name_en)
                                                    .Where(x => x.arrival_time == new DateTime() || x.release_time == new DateTime()).ToList();
                    //如果没有未签到或者未解除的异常记录 才允许重复添加log
                    if(error_logs.Count == 0)
                    {
                        //查询责任人员信息
                        person ps = DB.Queryable<person>()
                            .Where(x => x.id == ec.response_person_id).First();
                        //查询当前执行的工单
                        wo_machine_cur_log wocr = DB.Queryable<wo_machine_cur_log>()
                                        .Where(x => x.machine_id == machine_id).First();

                        error_log el = new error_log();
                        el.error_config_id = ec.id;
                        if (mc != null)
                            el.machine_name = mc.name_en;
                        el.tag_type_sub_name = ts.name_en;
                        if (ps != null)
                            el.responsible_name = ps.user_name;
                        if (wocr != null)
                        {
                            //查询主工单信息
                            wo_config wo = DB.Queryable<wo_config>()
                                            .Where(x => x.id == wocr.wo_config_id).First();
                            if (wo != null)
                            {
                                el.work_order = wo.work_order;
                                el.part_number = wo.part_num;
                            }
                        }
                        else
                        {
                            //如果是设备异常 是不需要在生产工单
                            if(ts.name_en != "equipment_error")
                                return false;
                        }
                        el.start_time = DateTime.Now;
                        return DB.Insertable<error_log>(el).ExecuteCommandIdentityIntoEntity();
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        //更新日志
        private bool Confirm(int log_id,string person_id)
        {
            //查询当前日志是否存在
            error_log el = DB.Queryable<error_log>().Where(x => x.id == log_id).First();
            //如果存在
            if(el != null)
            {
                person ps = DB.Queryable<person>().Where(x => x.id_num == person_id).First();
                if(ps != null)
                {
                    //如果有替代者 则判断替代者是否正确
                    if(el.substitutes != null)
                    {
                        if (ps.user_name == el.substitutes)
                        {
                            return DB.Updateable<error_log>().Where(x=>x.id == log_id).UpdateColumns(it => new error_log() { arrival_time = DateTime.Now }).ExecuteCommand() > 0;

                        }
                    }
                    else 
                    {
                        if (ps.user_name == el.responsible_name)
                        {
                            return DB.Updateable<error_log>().Where(x => x.id == log_id).UpdateColumns(it => new error_log() { arrival_time = DateTime.Now }).ExecuteCommand() > 0;
                        }
                    }
                }
            }
            return false;
        }


        //更新日志
        private bool QualityErrorRelease(int log_id, decimal count)
        {
            bool re = false;
            //查询当前日志是否存在
            error_log el = DB.Queryable<error_log>().Where(x => x.id == log_id).First();
            //如果存在
            if (el != null)
            {
                //查询当前执行的工单信息
                wo_config wo = DB.Queryable<wo_config>().Where(x=>x.work_order == el.work_order).First();
                //如果存在主工单
                if(wo != null)
                {
                    //如果是正在执行的工单 则更新当前你执行的设备/线的工单信息
                    if(wo.status == 2)
                    {
                        //查询设备工单日志
                        wo_machine_cur_log wmcl = DB.Queryable<wo_machine_cur_log>().Where(x => x.wo_config_id == wo.id).First();
                        //如果存在
                        if (wmcl != null)
                        {
                            re = DB.Updateable<wo_machine_cur_log>().Where(x => x.id == wmcl.id).UpdateColumns(it => new wo_machine_cur_log() { bad_quantity = count }).ExecuteCommand() > 0;
                        }
                        //查询设备工单日志
                        virtual_line_cur_log vlcl = DB.Queryable<virtual_line_cur_log>().Where(x => x.wo_config_id == wo.id).First();
                        if (vlcl != null)
                        {
                            re = re & DB.Updateable<virtual_line_cur_log>().Where(x => x.id == vlcl.id).UpdateColumns(it => new virtual_line_cur_log() { bad_quantity = count }).ExecuteCommand() > 0;
                        }
                    }
                    //如果是已经执行完的工单 则更新历史的设备/线的工单信息
                    else if (wo.status == 3)
                    {
                        //查询设备工单日志
                        wo_machine_log wml = DB.Queryable<wo_machine_log>().Where(x => x.wo_config_id == wo.id).First();
                        //如果存在
                        if (wml != null)
                        {
                            re = DB.Updateable<wo_machine_log>().Where(x => x.id == wml.id).UpdateColumns(it => new wo_machine_log() { bad_quantity = count }).ExecuteCommand() > 0;
                        }
                        //查询设备工单日志
                        virtual_line_log vll = DB.Queryable<virtual_line_log>().Where(x => x.wo_config_id == wo.id).First();
                        if (vll != null)
                        {
                            re = re & DB.Updateable<virtual_line_log>().Where(x => x.id == vll.id).UpdateColumns(it => new virtual_line_log() { bad_quantity = count }).ExecuteCommand() > 0;
                        }
                    }

                }
                decimal dif_time = CalTimeDifference((DateTime)el.arrival_time);
                return re& DB.Updateable<error_log>()
                         .Where(x=>x.id == log_id)
                         .UpdateColumns(it => new error_log() { release_time = DateTime.Now, defectives_count = count ,cost_time = dif_time })
                         .ExecuteCommand() > 0;
            }
            return false;
        }
        //更新日志
        private bool EquipmentErrorRelease(int log_id,int error_type_id,int error_type_detail_id)
        {
            //查询当前日志是否存在
            error_log el = DB.Queryable<error_log>().Where(x => x.id == log_id).First();
            error_type et = DB.Queryable<error_type>().Where(x => x.id == error_type_id).First();
            error_type_details etd = DB.Queryable<error_type_details>().Where(x => x.id == error_type_detail_id).First();

            //如果存在
            if (el != null)
            {
                decimal dif_time = CalTimeDifference((DateTime)el.arrival_time);
                return DB.Updateable<error_log>()
                          .Where(x => x.id == log_id)
                          .UpdateColumns(it => new error_log() { release_time = DateTime.Now, error_type_name =et.name_cn,error_type_detail_name=etd.name_cn,cost_time = dif_time })
                          .ExecuteCommand() > 0;
            }
            return false;
        }

        private bool MaterialRequestLog(int machine_id ,int count,string material_code, tag_type_sub ts)
        {

            //查看是否有配置异常配置 理论上最多只会有一条数据
            error_config ec = DB.Queryable<error_config>()
                                                    .Where(x => x.machine_id == machine_id)
                                                    .Where(x => x.tag_type_sub_id == ts.id)
                                                    .First();
            //如果已配置
            if (ec != null)
            {
                //查询设备设备名
                machine mc = DB.Queryable<machine>()
                                    .Where(x => x.id == machine_id).First();
                List<material_request_info> error_logs = DB.Queryable<material_request_info>()
                                .Where(x => x.machine_name == mc.name_en)
                                .Where(x => x.take_time == new DateTime()).ToList();
                //如果没有未签到或者未解除的异常记录 才允许重复添加log
                if (error_logs.Count == 0)
                {
                    //如果设备存在
                    if (mc != null)
                    {
                        //查询责任人员信息
                        person ps = DB.Queryable<person>()
                            .Where(x => x.id == ec.response_person_id).First();
                        //查询当前执行的工单
                        wo_machine_cur_log wocr = DB.Queryable<wo_machine_cur_log>()
                                        .Where(x => x.machine_id == machine_id).First();
                        material_request_info mri = new material_request_info();
                        if (wocr != null)
                        {
                            //查询主工单信息
                            wo_config wo = DB.Queryable<wo_config>()
                                            .Where(x => x.id == wocr.wo_config_id).First();
                            if (wo != null)
                            {
                                mri.work_order = wo.work_order;
                                mri.part_number = wo.part_num;
                            }
                        }

                        mri.error_config_id = ec.id;
                        if (mc != null)
                            mri.machine_name = mc.name_en;
                        mri.material_code = material_code;
                        mri.request_count = count;
                        mri.createtime = DateTime.Now;
                        if (ps != null)
                            mri.take_person_name = ps.user_name;

                        return DB.Insertable<material_request_info>(mri).ExecuteCommand()>0;
                    }
                }
                else
                {
                    return false;
                }

            }
            return false;
        }


        private bool UpdateMaterialLog(int machine_id, int log_id,string description)
        {
            //查询当前日志是否存在
            material_request_info mri = DB.Queryable<material_request_info>().Where(x => x.id == log_id).First();
            //如果存在
            if (mri != null)
            {
                decimal dif_time = CalTimeDifference((DateTime)mri.createtime);
                return DB.Updateable<material_request_info>()
                          .Where(x => x.id == log_id)
                          .UpdateColumns(it => new material_request_info() {take_time = DateTime.Now,cost_time =Convert.ToDecimal(dif_time) })
                          .ExecuteCommand() > 0;
            }
            return false;
        }


        MongoHelper mh = new MongoHelper();
        private void  SendMGMsg(string s,string t,int v)
        {
            MongoDbTag mongoDbTag = new MongoDbTag();
            mongoDbTag.ID = new ObjectId();
            mongoDbTag.s = s;
            mongoDbTag.t = t;
            mongoDbTag.v = v;
            mongoDbTag.ts = DateTime.Now;
            mh.InsertOne(mongoDbTag);
        }
        /// <summary>
        /// 计算时间差
        /// </summary>
        /// <param name="start_time"></param>
        /// <returns></returns>
        private decimal CalTimeDifference(DateTime start_time)
        {
            DateTime dt1 = DateTime.Now;
            DateTime dt2 = start_time;
            TimeSpan ts = dt1.Subtract(dt2);
            return Convert.ToDecimal(ts.TotalSeconds);
        }
    }
}
