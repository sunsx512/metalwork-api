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
        public new List<tag_info_detail> QueryableToList()
        {
            var list = DB.Queryable<tag_info_detail>()
            .Mapper((it) =>
            {
                List<tag_type_sub> tag_type_subs = DB.Queryable<tag_type_sub>().Where(x => x.id == it.tag_type_sub_id).ToList();
                List<tag_type> tag_types = DB.Queryable<tag_type>().Where(x => x.id == tag_type_subs.First().tag_type_id).ToList();
                List<machine> machines = DB.Queryable<machine>().Where(x => x.id == it.machine_id).ToList();
                it.id = it.id;
                it.machine = machines.FirstOrDefault();
                it.name = it.name;
                it.description = it.description;
                it.machine_id = it.machine_id;
                it.tag_type = tag_types.FirstOrDefault();
                it.tag_type_sub = tag_type_subs.FirstOrDefault();
            }).OrderBy(x=>x.id).ToList();
            return list;
        }
    }
}
