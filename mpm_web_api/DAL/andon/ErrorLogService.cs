using mpm_web_api.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL.andon
{
    public class ErrorLogService : BaseService<error_log>
    {
        DateTime dt = new DateTime();
        public  List<error_log> QueryableToListByStatus(string status)
        {
            List<error_log> list = new List<error_log>();
            if (status == "all")
            {
                list = DB.Queryable<error_log>().ToList();
            }
            else if(status == "penging")
            {
                list = DB.Queryable<error_log>().Where(x => x.arrival_time == dt).ToList();
            }
            else if (status == "processing")
            {
                list = DB.Queryable<error_log>().Where(x => x.arrival_time != dt && x.release_time == dt).ToList();
            }
            else if (status == "finished")
            {
                list = DB.Queryable<error_log>().Where(x => x.arrival_time != dt).ToList();
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
            el.start_time = DateTime.Now;
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
