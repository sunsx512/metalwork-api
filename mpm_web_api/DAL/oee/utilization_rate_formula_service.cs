using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mpm_web_api.db;
using mpm_web_api.model;

namespace mpm_web_api.DAL.oee
{
    public class utilization_rate_formula_service:SqlSugarBase
    {

        public List<T> GetList<T>() where T : new()
        {
            return DB.Queryable<T>().ToList();
        }

     

        public bool insert<T>(utilization_rate_formula Obj) where T : class, new()
        {
            var test = 0;
            try
            {
                if(DB.Queryable<T>().ToList()!=null)
                {
                    DB.Updateable(Obj).UpdateColumns(it => new { it.formula }).Where(it => it.id == Obj.id).ExecuteCommand();
                    test = 1;
                }
                else
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


        public bool update<T>(utilization_rate_formula obj) where T : class, new()
        {

            try
            {
                var result = DB.Updateable(obj).UpdateColumns(it => new { it.formula}).Where(it => it.id==obj.id).ExecuteCommand();
            }
            catch (Exception ex)
            {
                string mes = ex.Message;
            }

            return true;
        }
    }
}
