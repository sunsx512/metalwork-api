using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mpm_web_api.db;
using mpm_web_api.model;

namespace mpm_web_api.DAL.oee
{
    public class utilization_rate_workorder_service : SqlSugarBase
    {
        //根据设备id和工单 ，更新稼动率和时间
        // public bool update<T>(decimal utilization_rate_workorder, int machine_id) where T : class, new()
        public bool update<T>(utilization_rate_workorder obj) where T : class, new()
        {

            try
            {
                var result = DB.Updateable(obj).UpdateColumns(it => new { it.utilization_rate, DateTime.Now }).Where(it => it.machine_id == obj.machine_id && it.work_order == obj.work_order).ExecuteCommand();
               //  var result = DB.Updateable<utilization_rate_workorder>().SetColumns(it => it.utilization_rate == utilization_rate && it.insert_time==DateTime.Now).Where(it => it.machine_id == machine_id && it.work_order==work_order).ExecuteCommand();
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
        public List<utilization_rate_workorder> GetList<T>(utilization_rate_workorder obj) where T : new()
        {
            var query = DB.Queryable<utilization_rate_workorder>().Where(x => x.machine_id == obj.machine_id && x.work_order==obj.work_order).ToList();
            return query;
        }

    }
}

