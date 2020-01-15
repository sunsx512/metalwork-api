using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;
using mpm_web_api.model;
using mpm_web_api.Common;
using mpm_web_api.db;

namespace mpm_web_api.DAL.oee
{
    public class tag_time_shift_service : SqlSugarBase
    {
        public bool update<T>(tag_time_shift obj) where T : class, new()
        {

            try
            {
                //var result = DB.Updateable(obj).UpdateColumns(it => new { it.utilization_rate, it.insert_time }).Where(it => it.machine_id == obj.machine_id).ExecuteCommand();
                var result = DB.Updateable<tag_time_shift>().SetColumns(it => it.duration_time == obj.duration_time).Where(it => it.machine_id == obj.machine_id && it.shift == obj.shift && it.status_name == obj.status_name).ExecuteCommand();
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
        public List<tag_time_shift> GetList<T>(int machine_id) where T : new()
        {
            var query = DB.Queryable<tag_time_shift>().Where(x => x.machine_id == machine_id).ToList();
            return query;
        }
    }
}
