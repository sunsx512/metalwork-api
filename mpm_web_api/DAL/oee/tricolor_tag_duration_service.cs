using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mpm_web_api.db;
using SqlSugar;
using mpm_web_api.model;
using mpm_web_api.Common;

namespace mpm_web_api.DAL.oee
{
    public class tricolor_tag_duration_service : SqlSugarBase
    {
        public List<T> GetList<T>() where T : new()
        {
            return DB.Queryable<T>().ToList();
        }

        public List<tricolor_tag_duration> GetList<T>(int machine_id) where T : new()
        {
            var query = DB.Queryable<tricolor_tag_duration>().Where(x => x.machine_id == machine_id).ToList();
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


        public bool update<T>(tricolor_tag_duration obj) where T : class, new()
        {

            try
            {
                var result = DB.Updateable(obj).UpdateColumns(it => new { it.status_name, it.insert_time }).Where(it => it.machine_id == obj.machine_id).ExecuteCommand();
                //var result = DB.Updateable<tricolor_tag_log>().SetColumns(it => it.insert_time == DateTime.Now).Where(it => it.srp_code == srp_code).ExecuteCommand();
            }
            catch (Exception ex)
            {
                string mes = ex.Message;
            }

            return true;
        }


        public bool Delete<T>(int id)
        {
            return DB.Deleteable<tricolor_tag_duration>(id).ExecuteCommand() > 0;
        }

        public bool Delete<T>(string timespan)
        {
            var res = 0;
            try
            {
                res = DB.Deleteable<tricolor_tag_duration>(it => it.insert_time < DateTime.Now).ExecuteCommand();
            }
            catch (Exception ex)
            {

            }
            return res > 0;

        }
    }
}
