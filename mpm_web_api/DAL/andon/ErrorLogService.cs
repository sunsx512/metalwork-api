using mpm_web_api.Common;
using mpm_web_api.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL.andon
{
    public class ErrorLogService : BaseService<error_log>
    {
        public  List<error_log> QueryableToListByStatus(int status)
        {
            List<error_log> list = new List<error_log>();
            switch (status)
            {
                case 0: list = DB.Queryable<error_log>().ToList();break;
                case 1: list = DB.Queryable<error_log>().Where(x => x.arrival_time == null).ToList();; break;
                case 2: list = DB.Queryable<error_log>().Where(x => x.arrival_time != null && x.release_time == null).ToList(); break;
                case 3: list = DB.Queryable<error_log>().Where(x => x.arrival_time != null).ToList(); break;
            }
            return list;
        }


        public List<error_log_detail> QueryableDetailToListByStatus(int status)
        {
            List<error_log_detail> list = new List<error_log_detail>();
            switch (status)
            {
                case 0: list = DB.Queryable<error_log_detail>()
                                .Mapper((it) =>
                                {
                                    List<error_config> error_configs = DB.Queryable<error_config>().Where(x => x.id == it.error_config_id).ToList();
                                    it.error_config = error_configs.FirstOrDefault();
                                }).ToList(); break;
                case 1: list = DB.Queryable<error_log_detail>().Where(x => x.arrival_time == null)
                                .Mapper((it) =>
                                {
                                    List<error_config> error_configs = DB.Queryable<error_config>().Where(x => x.id == it.error_config_id).ToList();
                                    it.error_config = error_configs.FirstOrDefault();
                                }).ToList(); break; 
                case 2: list = DB.Queryable<error_log_detail>().Where(x => x.arrival_time != null && x.release_time == null)
                                .Mapper((it) =>
                                {
                                    List<error_config> error_configs = DB.Queryable<error_config>().Where(x => x.id == it.error_config_id).ToList();
                                    it.error_config = error_configs.FirstOrDefault();
                                }).ToList(); break;
                case 3: list = DB.Queryable<error_log_detail>().Where(x => x.arrival_time != null)
                                .Mapper((it) =>
                                {
                                    List<error_config> error_configs = DB.Queryable<error_config>().Where(x => x.id == it.error_config_id).ToList();
                                    it.error_config = error_configs.FirstOrDefault();
                                }).ToList(); break;
            }
            return list;
        }


        public List<error_log_detail> QueryableDetailToListByStatus(int type,int status)
        {
            List<error_log_detail> list = new List<error_log_detail>();
            switch (status)
            {
                case 0:
                    list = DB.Queryable<error_log_detail>()
                            .Mapper((it) =>
                            {
                                List<error_config> error_configs = DB.Queryable<error_config>().Where(x => x.id == it.error_config_id).ToList();
                                it.error_config = error_configs.First();
                            }).ToList(); break;
                case 1:
                    list = DB.Queryable<error_log_detail>().Where(x => x.arrival_time == null)
                            .Mapper((it) =>
                            {
                                List<error_config> error_configs = DB.Queryable<error_config>().Where(x => x.id == it.error_config_id).ToList();
                                it.error_config = error_configs.First();
                            }).ToList(); break;
                case 2:
                    list = DB.Queryable<error_log_detail>().Where(x => x.arrival_time != null && x.release_time == null)
                            .Mapper((it) =>
                            {
                                List<error_config> error_configs = DB.Queryable<error_config>().Where(x => x.id == it.error_config_id).ToList();
                                it.error_config = error_configs.First();
                            }).ToList(); break;
                case 3:
                    list = DB.Queryable<error_log_detail>().Where(x => x.arrival_time != null)
                            .Mapper((it) =>
                            {
                                List<error_config> error_configs = DB.Queryable<error_config>().Where(x => x.id == it.error_config_id).ToList();
                                it.error_config = error_configs.First();
                            }).ToList(); break;
            }
            switch (type)
            {
                case 0: list = list.Where(x => x.tag_type_sub_name == "equipment_error").ToList(); break;
                case 1: list = list.Where(x => x.tag_type_sub_name == "quality_error").ToList(); break;
                case 2: list = list.Where(x => x.tag_type_sub_name == "material_require").ToList(); break;
            }
            return list;
        }


        public List<error_log> QueryableToListByStatusAndType(int type,int status)
        {
            List<error_log> list = new List<error_log>();
            switch (status)
            {
                case 0: list = DB.Queryable<error_log>().ToList(); break;
                case 1: list = DB.Queryable<error_log>().Where(x => x.arrival_time == null).ToList();  break;
                case 2: list = DB.Queryable<error_log>().Where(x => x.arrival_time != null && x.release_time == null).ToList(); break;
                case 3: list = DB.Queryable<error_log>().Where(x => x.arrival_time != null).ToList(); break;
            }
            switch (type)
            {
                case 0: list = list.Where(x => x.tag_type_sub_name == "equipment_error").ToList(); break;
                case 1: list = list.Where(x => x.tag_type_sub_name == "quality_error").ToList(); break;
                case 2: list = list.Where(x => x.tag_type_sub_name == "material_require").ToList(); break;
            }
            return list;
        }


        public List<error_log> QueryableToListByMahcine(string machine ,string work_order)
        {
            List<error_log> list = new List<error_log>();
            list = DB.Queryable<error_log>().Where(x => x.machine_name == machine).Where(x => x.work_order == work_order).ToList();
            return list;
        }

        public bool AddErrorLog(int config_id, string error_name, string machine_name, string responsible_name, string work_order, string part_number)
        {
            error_log el = new error_log();
            el.error_config_id = config_id;
            el.machine_name = machine_name;
            el.tag_type_sub_name = error_name;
            el.responsible_name = responsible_name;
            el.work_order = work_order;
            el.part_number = part_number;
            el.start_time = DateTime.Now.AddHours(GlobalVar.db_time_zone);
            return DB.Insertable<error_log>(el).ExecuteCommandIdentityIntoEntity();        
        }

        [Obsolete]
        public bool UpdataHandleTime(int id,int type)
        {
            if(type == 0)
            {
                return DB.Updateable<error_log>().Where(x => x.id == id).UpdateColumns(x => x.arrival_time == DateTime.UtcNow).ExecuteCommand() > 0;
            }
            else if (type == 1)
            {
                return DB.Updateable<error_log>().Where(x => x.id == id).UpdateColumns(x => x.release_time == DateTime.UtcNow).ExecuteCommand() > 0;
            }       
            else
            {
                return false;
            }
        }


        public bool UpdataSubstitutes(int id, string substitutes)
        {
             return DB.Updateable<error_log>().Where(x => x.id == id).UpdateColumns(x => x.substitutes == substitutes).ExecuteCommand() > 0;
        }
    }
}
