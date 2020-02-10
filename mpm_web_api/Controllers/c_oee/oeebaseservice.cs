using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mpm_web_api.db;
using SqlSugar;
using mpm_web_api.model;

namespace mpm_web_api.Controllers.oee
{
    public class oeebaseservice : SqlSugarBase
    {
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
        public List<utilization_rate_day> GetList<T>(int machine_id) where T : new()
        {
            var query = DB.Queryable<utilization_rate_day>().Where(x => x.machine_id == machine_id).ToList();
            return query;
        }
    }
}
