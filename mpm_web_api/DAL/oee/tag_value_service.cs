using mpm_web_api.db;
using mpm_web_api.model.m_oee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL.oee
{
    public class tag_value_service : SqlSugarBase
    {
        public List<tag_info_value> GetList() 
        {
            return DB.Queryable<tag_info_value>().ToList();
        }
    }
}
