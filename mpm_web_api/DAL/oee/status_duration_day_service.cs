using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mpm_web_api.db;
using mpm_web_api.model;
using SqlSugar;
using mpm_web_api.Common;


namespace mpm_web_api.DAL.oee
{
    public class status_duration_day_service : SqlSugarBase
    {
        public bool update<T>(status_duration_day obj) where T : class, new()
        {

            try
            {
               // var result = DB.Updateable(obj).UpdateColumns(it => new { it.utilization_rate, it.insert_time }).Where(it => it.machine_id == obj.machine_id).ExecuteCommand();
                 var result = DB.Updateable<status_duration_day>().SetColumns(it => it.duration_time == obj.duration_time ).Where(it => it.upper_id == obj.upper_id && it.status_name==obj.status_name).ExecuteCommand();
            }
            catch (Exception ex)
            {
                string mes = ex.Message;
            }

            return true;
        }

        // 按主键更新全部
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

        //插入
        public bool insert<T>(T Obj) where T : class, new()
        {

            try
            {
                var t1 = DB.Insertable(Obj).ExecuteCommand();
            }
            catch (Exception ex)
            {
                string mes = ex.Message;
            }

            return true;
        }


        //查询全部
        public List<T> GetList<T>() where T : new()
        {
            return DB.Queryable<T>().ToList();
        }

        //按字段查询
        public List<status_duration_day> GetList<T>(int upper_id) where T : new()
        {
            var query = DB.Queryable<status_duration_day>().Where(x => x.upper_id == upper_id).ToList();
            return query;
        }
    }
}
