using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mpm_web_api.model;
using mpm_web_api.db;

namespace mpm_web_api.DAL
{
    public class srp_inner_log_service: SqlSugarBase
    {

        public List<T> GetList<T>() where T : new()
        {
            return DB.Queryable<T>().ToList();
        }

        public List<srp_inner_log> GetList<T>(string fields) where T : new()
        {
            var query = DB.Queryable<srp_inner_log>().Where(x => x.srp_code == fields).ToList();
            return query;
       
        }

        public bool insert<T>(T Obj) where T : class, new()
        {
            var test = 0;
            try
            {
                test = DB.Insertable(Obj).ExecuteCommand();
            }
            catch (Exception ex)
            {
                string mes = ex.Message;
            }

            return test > 0;
        }

        public bool update<T>(T Obj) where T : class, new()
        {

            try
            {
                var t1 = DB.Updateable(Obj).ExecuteCommand();
            }
            catch (Exception ex)
            {
                string mes = ex.Message;
            }

            return true;
        }

   
        public bool update<T>(string srp_code) where T : class, new()
        {

            try
            {
              //  var result = DB.Updateable(obj).UpdateColumns(it => new { it.srp_code, it.insert_time }).Where(it => it.srp_code == obj.srp_code).ExecuteCommand();
                 var result = DB.Updateable<srp_inner_log>().SetColumns(it => it.insert_time == DateTime.Now ).Where(it => it.srp_code == srp_code).ExecuteCommand();
            }
            catch (Exception ex)
            {
                string mes = ex.Message;
            }

            return true;
        }
    }
}
