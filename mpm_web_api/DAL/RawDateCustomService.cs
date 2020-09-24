using mpm_web_api.db;
using mpm_web_api.model;
using mpm_web_api.model.m_common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL
{
    public class RawDateCustomService : SqlSugarBase
    {
        public List<raw_date_custom> Query(string tag_name,DateTime start_time,DateTime end_time)
        {
            tag_info tag = DB.Queryable<tag_info>().Where(x => x.name == tag_name)?.First();
            List<raw_date_custom> raw_date_customs = null;
            if(tag != null)
            {
                raw_date_customs = DB.Queryable<raw_date_custom>()
                                    .Where(x => x.tag_type_sub_id == tag.tag_type_sub_id && x.machine_id == tag.machine_id)
                                    .Where(x => x.insert_time >= start_time && x.insert_time <= end_time)
                                    .ToList();                                                             
            }
            return raw_date_customs;
        }
    }
}
