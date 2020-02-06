using mpm_web_api.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL.andon
{
    public class ErrorLogService : BaseService<error_log>
    {
        public  List<error_log> QueryableToList(string status)
        {
            List<error_log> list = new List<error_log>();
            if (status == "all")
            {
                list = DB.Queryable<error_log>().ToList();
            }
            else if(status == "penging")
            {
                list = DB.Queryable<error_log>().Where(x => x.arrival_time == null).ToList();
            }
            else if (status == "processing")
            {
                list = DB.Queryable<error_log>().Where(x => x.arrival_time != null && x.release_time == null).ToList();
            }
            else if (status == "finished")
            {
                list = DB.Queryable<error_log>().Where(x => x.arrival_time != null).ToList();
            }
            return list;
        }
    }
}
