using mpm_web_api.db;
using mpm_web_api.model;
using mpm_web_api.model.m_oee;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL
{
    public class OnsiteMachineStatusService : SqlSugarBase
    {
        public List<tricolor_tag_status> QueryableDetailToList()
        {
            List< tricolor_tag_status > list= DB.Queryable<tricolor_tag_status, machine>((s, t) => new object[] {
                JoinType.Left,s.machine_id==t.id
            }).Select((s, t) => new tricolor_tag_status { machine_name = t.name_cn, status_name=s.status_name, insert_time=s.insert_time }).ToList();
            return list;
        }
    }
}
