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
    public class status_duration_order_service : SqlSugarBase
    {
        public bool update<T>(status_duration_order obj) where T : class, new()
        {

            try
            {
                var result = DB.Updateable<status_duration_order>().SetColumns(it => it.duration_time == obj.duration_time).Where(it => it.upper_id == obj.upper_id && it.status_name == obj.status_name).ExecuteCommand();
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
        public List<status_duration_order> GetList<T>(int upper_id) where T : new()
        {
            var query = DB.Queryable<status_duration_order>().Where(x => x.upper_id == upper_id).ToList();
            return query;
        }
    }
}
