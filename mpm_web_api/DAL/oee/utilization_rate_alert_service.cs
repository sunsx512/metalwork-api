using mpm_web_api.db;
using mpm_web_api.model.m_oee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL.oee
{
    public class utilization_rate_alert_service: SqlSugarBase
    {
        public List<utilization_rate_alert> GetList<T>() 
        {
            return DB.Queryable<utilization_rate_alert>().Where(x=>x.utilization_rate_type == "").ToList();
        }

    }
}
