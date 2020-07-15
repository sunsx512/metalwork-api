using MongoDB.Bson;
using MongoDB.Driver;
using mpm_web_api.Common;
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
                        SendMGMsg(s, t,(int)GlobalVar.Error_handle.trigger);
                    }
                    else
                    {
                        string s = "quality_error";
                        machine mc = DB.Queryable<machine>().Where(x => x.id == machine_id).First();
                        string t = mc.name_en;
                        SendMGMsg(s, t, (int)GlobalVar.Error_handle.trigger);
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
                        SendMGMsg(s, t, (int)GlobalVar.Error_handle.trigger);
                    }
                    else
                    {
                        string s = "equipment_error";
                        machine mc = DB.Queryable<machine>().Where(x => x.id == machine_id).First();
                        string t = mc.name_en;
                        SendMGMsg(s, t, (int)GlobalVar.Error_handle.trigger);
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
                        SendMGMsg(s, t, (int)GlobalVar.Error_handle.trigger);
                    }
                    else
                    {
                        string s = "material_require";
                        machine mc = DB.Queryable<machine>().Where(x => x.id == machine_id).First();
                        string t = mc.name_en;
                        SendMGMsg(s, t, (int)GlobalVar.Error_handle.trigger);
                    }
                    return true;
                }
                
            }
            return false;
        }

        public bool QualityConfirm(int machine_id, int log_id,string person_id)
        {
            //查询绑定的异常触发按钮
            tag_type_sub tag_Type_Sub = DB.Queryable<tag_type_sub>().Where(x => x.name_en == "quality_error").First();
            if (tag_Type_Sub != null)
            {
                if (Confirm(log_id, person_id))
                {
                    tag_info tag_Info = DB.Queryable<tag_info>()
                                                    .Where(x => x.machine_id == machine_id)
                                                    .Where(x => x.tag_type_sub_id == tag_Type_Sub.id).First();
                    //如果有该按钮则需要
                    if (tag_Info != null)
                    {
                        string s = tag_Info.name.Split(':')[0];
                        string t = tag_Info.name.Split(':')[1];
                        SendMGMsg(s, t,(int)GlobalVar.Error_handle.sign_in);
                    }
                    return true ;
                }

            }
            return false;          
        }

        public bool EquipmentConfirm(int machine_id, int log_id, string person_id)
        {
            //查询绑定的异常触发按钮
            tag_type_sub tag_Type_Sub = DB.Queryable<tag_type_sub>().Where(x => x.name_en == "equipment_error").First();
            if (tag_Type_Sub != null)
            {
                if (Confirm(log_id, person_id))
                {
                    tag_info tag_Info = DB.Queryable<tag_info>()
                                                .Where(x => x.machine_id == machine_id)
                                                .Where(x => x.tag_type_sub_id == tag_Type_Sub.id).First();
                    //如果有该按钮则需要
                    if (tag_Info != null)
                    {
                        string s = tag_Info.name.Split(':')[0];
                        string t = tag_Info.name.Split(':')[1];
                        SendMGMsg(s, t, (int)GlobalVar.Error_handle.sign_in);
                    }

                    return true;
                }
            }
            return false;

        }

        public bool Qualityrelease(int machine_id,int log_id, int error_type_id, int error_type_detail_id, decimal count)
        {
            //查询绑定的异常触发按钮
            tag_type_sub tag_Type_Sub = DB.Queryable<tag_type_sub>().Where(x => x.name_en == "quality_error").First();
            if (tag_Type_Sub != null)
            {
                if (QualityErrorRelease(log_id, error_type_id, error_type_detail_id, count))
                {
                    tag_info tag_Info = DB.Queryable<tag_info>()
                                                .Where(x => x.machine_id == machine_id)
                                                .Where(x => x.tag_type_sub_id == tag_Type_Sub.id).First();
                    //如果有该按钮则需要
                    if (tag_Info != null)
                    {
                        string s = tag_Info.name.Split(':')[0];
                        string t = tag_Info.name.Split(':')[1];
                        SendMGMsg(s, t, (int)GlobalVar.Error_handle.release);
                    }
                    return true;
                }
            }
            return false;
        }
        public bool EquipmentRelease(int machine_id,int log_id, int error_type_id, int error_type_detail_id)
        {
            //查询绑定的异常触发按钮
            tag_type_sub tag_Type_Sub = DB.Queryable<tag_type_sub>().Where(x => x.name_en == "equipment_error").First();
            if (tag_Type_Sub != null)
            {
                if (EquipmentErrorRelease(machine_id,log_id, error_type_id, error_type_detail_id))
                {
                    tag_info tag_Info = DB.Queryable<tag_info>()
                                               .Where(x => x.machine_id == machine_id)
                                               .Where(x => x.tag_type_sub_id == tag_Type_Sub.id).First();
                    //如果有该按钮则需要
                    if (tag_Info != null)
                    {
                        string s = tag_Info.name.Split(':')[0];
                        string t = tag_Info.name.Split(':')[1];
                        SendMGMsg(s, t, (int)GlobalVar.Error_handle.release);
                    }
                    return true;
                }
            }
            return false;          
        }

        public bool MaterialRequestRelease(int machine_id, int log_id, string description)
        {
            //查询绑定的物料呼叫按钮
            tag_type_sub tag_Type_Sub = DB.Queryable<tag_type_sub>().Where(x => x.name_en == "material_require").First();
            if (tag_Type_Sub != null)
            {
                if (UpdateMaterialLog(machine_id, log_id, description))
                {
                    tag_info tag_Info = DB.Queryable<tag_info>()
                                               .Where(x => x.machine_id == machine_id)
                                               .Where(x => x.tag_type_sub_id == tag_Type_Sub.id).First();
                    //如果有该按钮则需要
                    if (tag_Info != null)
                    {
                        string s = tag_Info.name.Split(':')[0];
                        string t = tag_Info.name.Split(':')[1];
                        SendMGMsg(s, t, (int)GlobalVar.Error_handle.release);
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
                                                    .Where(x => x.arrival_time == null || x.release_time == null).ToList();
                    //如果没有未签到或者未解除的异常记录 才允许重复添加log
                    if(error_logs.Count == 0)
                    {
                        //查询当前执行的工单
                        wo_machine_cur_log wocr = DB.Queryable<wo_machine_cur_log>()
                                        .Where(x => x.machine_id == machine_id).First();

                        error_log el = new error_log();
                        el.error_config_id = ec.id;
                        if (mc != null)
                            el.machine_name = mc.name_en;
                        el.tag_type_sub_name = ts.name_en;
                        el.responsible_name = ec.response_person_id;
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
                        el.start_time = DateTime.Now.AddHours(GlobalVar.time_zone);
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
        [Obsolete]
        private bool Confirm(int log_id,string person_card)
        {
            //查询当前日志是否存在
            error_log el = DB.Queryable<error_log>().Where(x => x.id == log_id).First();
            //如果存在
            if(el != null)
            {
                person ps = DB.Queryable<person>().Where(x => x.id_num == person_card).First();
                if(ps != null)
                {
                    //如果有替代者 则判断替代者是否正确
                    if(el.substitutes != null)
                    {
                        if (ps.id == el.substitutes)
                        {
                            DateTime now = DateTime.Now.AddHours(GlobalVar.time_zone);
                            return DB.Updateable<error_log>().Where(x=>x.id == log_id).UpdateColumns(it => new error_log() { arrival_time = now,status =1 }).ExecuteCommand() > 0;
                        }
                    }
                    else 
                    {
                        if (ps.id == el.responsible_name)
                        {
                            DateTime now = DateTime.Now.AddHours(GlobalVar.time_zone);
                            return DB.Updateable<error_log>().Where(x => x.id == log_id).UpdateColumns(it => new error_log() { arrival_time = now , status = 1 }).ExecuteCommand() > 0;
                        }
                    }
                }
            }
            return false;
        }


        //更新日志
        private bool QualityErrorRelease(int log_id, int error_type_id, int error_type_detail_id, decimal count)
        {
            bool re = false;
            //查询当前日志是否存在
            error_log el = DB.Queryable<error_log>().Where(x => x.id == log_id).First();
            error_type et = DB.Queryable<error_type>().Where(x => x.id == error_type_id)?.First();
            error_type_details etd = DB.Queryable<error_type_details>().Where(x => x.id == error_type_detail_id)?.First();
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
                decimal dif_time = CalTimeDifference((DateTime)el.start_time);
                DateTime now = DateTime.Now.AddHours(GlobalVar.time_zone);
                if(et == null)
                {
                    return re & DB.Updateable<error_log>()
                             .Where(x => x.id == log_id)
                             .UpdateColumns(it => new error_log() { release_time = now, error_type_name = null, error_type_detail_name = null, defectives_count = count, cost_time = dif_time, status = 2 })
                             .ExecuteCommand() > 0;
                }
                else
                {
                    return re & DB.Updateable<error_log>()
                             .Where(x => x.id == log_id)
                             .UpdateColumns(it => new error_log() { release_time = now, error_type_name = et.name_en, error_type_detail_name = etd.name_en, defectives_count = count, cost_time = dif_time, status = 2 })
                             .ExecuteCommand() > 0;
                }

            }
            return false;
        }
        //更新日志
        private bool EquipmentErrorRelease(int machine_id,int log_id,int error_type_id,int error_type_detail_id)
        {
            //判定设备异常是否可以被解除
            if(IsErrorRelease(machine_id))
            {
                //查询当前日志是否存在
                error_log el = DB.Queryable<error_log>().Where(x => x.id == log_id).First();
                error_type et = DB.Queryable<error_type>().Where(x => x.id == error_type_id)?.First();
                error_type_details etd = DB.Queryable<error_type_details>().Where(x => x.id == error_type_detail_id)?.First();

                //如果存在
                if (el != null)
                {
                    decimal dif_time = CalTimeDifference((DateTime)el.start_time);
                    DateTime now = DateTime.Now.AddHours(GlobalVar.time_zone);
                    if (et == null)
                    {
                        return DB.Updateable<error_log>()
                              .Where(x => x.id == log_id)
                              .UpdateColumns(it => new error_log() { release_time = now, error_type_name = null, error_type_detail_name = null, cost_time = dif_time, status = 2 })
                              .ExecuteCommand() > 0;
                    }
                    else
                    {
                        return DB.Updateable<error_log>()
                              .Where(x => x.id == log_id)
                              .UpdateColumns(it => new error_log() { release_time = now, error_type_name = et.name_cn, error_type_detail_name = etd.name_cn, cost_time = dif_time, status = 2 })
                              .ExecuteCommand() > 0;
                    }


                }
                return false;
            }
            else
            {
                return false;
            }
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
                                .Where(x => x.take_time == null).ToList();
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
                        mri.createtime = DateTime.Now.AddHours(GlobalVar.time_zone);
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
                DateTime now = DateTime.Now.AddHours(GlobalVar.time_zone);
                return DB.Updateable<material_request_info>()
                          .Where(x => x.id == log_id)
                          .UpdateColumns(it => new material_request_info() {take_time = now, cost_time =Convert.ToDecimal(dif_time), status = 2 })
                          .ExecuteCommand() > 0;
            }
            return false;
        }

        MongoHelper MongoHelper = new MongoHelper();
        /// <summary>
        /// 设备异常是否可以解除
        /// </summary>
        /// <returns></returns>
        public bool IsErrorRelease(int machine_id)
        {
            //
            tag_type_sub tag_Type_Sub = DB.Queryable<tag_type_sub>().Where(x => x.name_en == "machine_error_release").First();
            tag_info tag_Info = DB.Queryable<tag_info>().Where(x => x.machine_id == machine_id && x.tag_type_sub_id == tag_Type_Sub.id).First();
            List<MongoDbTag> list = new List<MongoDbTag>();
            if (tag_Info == null)
            {
                //如果没有绑定该点 则默认可以解除
                return true;
            }
            else
            {
                //取最近五分钟内的数据
                DateTime end_time = DateTime.Now;
                DateTime start_time = end_time.AddMinutes(-5);
                string scada = tag_Info.name.Split(':')[0];
                string tag = tag_Info.name.Split(':')[1];
                if (GlobalVar.IsCloud)
                {
                    var filterBuilder1 = Builders<CloudMongoDbTag>.Filter;
                    var filter1 = filterBuilder1.And(filterBuilder1.Gt(x => x.ts, start_time),
                                                        filterBuilder1.Lte(x => x.ts, end_time),
                                                        filterBuilder1.Eq(x => x.t, tag));
                    //抓取C/T相关的点位
                    List<CloudMongoDbTag> cmdb = MongoHelper.GetList<CloudMongoDbTag>("datahub_HistRawData_" + scada, filter1);
                    if (cmdb != null)
                    {
                        foreach (CloudMongoDbTag cloudMongoDbTag in cmdb)
                        {
                            MongoDbTag mongoDbTag = new MongoDbTag();
                            mongoDbTag.ID = cloudMongoDbTag.ID;
                            mongoDbTag.s = scada;
                            mongoDbTag.t = cloudMongoDbTag.t;
                            mongoDbTag.ts = cloudMongoDbTag.ts;
                            mongoDbTag.v = Convert.ToInt32(cloudMongoDbTag.v);
                            list.Add(mongoDbTag);
                        }
                    }

                }
                else
                {
                    var filterBuilder1 = Builders<MongoDbTag>.Filter;
                    var filter1 = filterBuilder1.And(filterBuilder1.Gt(x => x.ts, start_time),
                                                        filterBuilder1.Lte(x => x.ts, end_time),
                                                        filterBuilder1.Eq(x => x.s, scada),
                                                        filterBuilder1.Eq(x => x.t, tag));
                    //抓取C/T相关的点位
                    list = MongoHelper.GetList<MongoDbTag>("scada_HistRawData", filter1);
                }
            }
            if(list.Count > 0)
            {
                var last_row = list.Last();
                //1:设备异常允许被解除  0:设备异常不允许被解除
                if (list.Last().v == 1)
                    return true;
                else
                    return false;
            }
            return false;
        }

        MQTTHelper mh = new MQTTHelper();
        private void SendMGMsg(string s, string t, int v)
        {
            //MongoDbTag mongoDbTag = new MongoDbTag();
            //mongoDbTag.ID = new ObjectId();
            //mongoDbTag.s = s;
            //mongoDbTag.t = t;
            //mongoDbTag.v = v;
            //mongoDbTag.ts = DateTime.Now;
            //mh.InsertForMetalwork(mongoDbTag);
            string dt = Convert.ToString(DateTime.Now.AddHours(GlobalVar.time_zone));
            string str = "{\"d\":{\"Cmd\":\"WV\",\"Val\":{\""+ s + ":" + t + "\":"+v.ToString()+"}},\"ts\":\""+ dt + "\"}";
            mh.SendAsync(str);
        }
        /// <summary>
        /// 计算时间差
        /// </summary>
        /// <param name="start_time"></param>
        /// <returns></returns>
        private decimal CalTimeDifference(DateTime start_time)
        {
            DateTime dt1 = DateTime.Now.AddHours(GlobalVar.time_zone);
            DateTime dt2 = start_time;
            TimeSpan ts = dt1.Subtract(dt2);
            return Convert.ToDecimal(ts.TotalSeconds);
        }
    }
}
