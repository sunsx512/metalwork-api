using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mpm_web_api.model;
using mpm_web_api.db;

namespace mpm_web_api.DAL.oee
{
    public class status_setting_service: SqlSugarBase
    {
        public List<T> GetList<T>() where T : new()
        {
            return DB.Queryable<T>().ToList();
        }

        public List<status_setting> GetList<T>(string fields) where T : new()
        {
            var query = DB.Queryable<status_setting>().Where(x => x.status_name == fields).ToList();
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

        //public bool update<T>(T Obj) where T : class, new()
        //{

        //    try
        //    {
        //        var t1 = DB.Updateable(Obj).ExecuteCommand();
        //    }
        //    catch (Exception ex)
        //    {
        //        string mes = ex.Message;
        //    }

        //    return true;
        //}


        public bool update<T>(status_setting obj) where T : class, new()
        {

            try
            {
                //  var result = DB.Updateable(obj).UpdateColumns(it => new { it.srp_code, it.insert_time }).Where(it => it.srp_code == obj.srp_code).ExecuteCommand();
                var result = DB.Updateable<status_setting>().SetColumns(it => it.value == obj.value).Where(it => it.status_name == obj.status_name).ExecuteCommand();
            }
            catch (Exception ex)
            {
                string mes = ex.Message;
            }

            return true;
        }

    }
}
