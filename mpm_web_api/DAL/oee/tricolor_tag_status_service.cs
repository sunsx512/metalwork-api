using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mpm_web_api.db;
using SqlSugar;
using mpm_web_api.model;

namespace mpm_web_api.DAL.oee
{
    public class tricolor_tag_status_service: SqlSugarBase
    {

        public List<T> GetList<T>() where T : new()
        {
            return DB.Queryable<T>().ToList();
        }

        public List<tricolor_tag_status> GetList<T>(int machine_id) where T : new()
        {
            var query = DB.Queryable<tricolor_tag_status>().Where(x => x.machine_id == machine_id).ToList();
            return query;
        }

        public bool insert<T>(T Obj,int value) where T : class, new()
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


        public bool update<T>(tricolor_tag_status obj) where T : class, new()
        {

            try
            {
                //  var result = DB.Updateable(obj).UpdateColumns(it => new { it.srp_code, it.insert_time }).Where(it => it.srp_code == obj.srp_code).ExecuteCommand();
                var result = DB.Updateable<tricolor_tag_status>().SetColumns(it => it.duration_time == obj.duration_time).Where(it => it.status_name == obj.status_name).ExecuteCommand();
            }
            catch (Exception ex)
            {
                string mes = ex.Message;
            }

            return true;
        }

    }
}
