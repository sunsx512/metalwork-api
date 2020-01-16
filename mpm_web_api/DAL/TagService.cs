using mpm_web_api.model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL
{
    public class TagService:BaseService<tag_info>
    {
        public new object QueryableToList()
        {
            var list =  DB.Queryable<tag_info,tag_type, tag_type_sub,machine>((s1, s2, s3,s4) => new object[] {
            JoinType.Left,s1.tag_type_sub_id == s2.id,
            JoinType.Left,s2.id == s3.tag_type_id,
            JoinType.Left,s1.machine_id == s3.id})
            .Select((s1, s2, s3,s4) => new
            {
                id = s1.id,
                tag_name = s1.name,
                tag_type = s2,
                tag_type_sub = s3,
                machine = s4
            }).ToList();
            return list;
        }
    }
}
