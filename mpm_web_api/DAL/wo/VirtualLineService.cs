using mpm_web_api.model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL
{
    public class VirtualLineService:BaseService<virtual_line>
    {
        public new List<virtual_line_detail> QueryableToList()
        {
            //var list = DB.Queryable<virtual_line, wo_machine>((s1, s2) => new object[] {
            //JoinType.Left,s1.id == s2.virtual_line_id})
            //.Select((s1, s2) => new
            //{
            //    id = s1.id,
            //    name_cn = s1.name_cn,
            //    name_en = s1.name_en,
            //    name_tw = s1.name_tw,
            //    wo_machine = s2,
            //}).ToList();
            var list = DB.Queryable<virtual_line_detail>()
                   .Mapper((it) =>
                   {
                       var machinelist = DB.Queryable<wo_machine_detail>().Where(x => x.virtual_line_id == it.id).ToList();
                       it.id = it.id;
                       it.name_cn = it.name_cn;
                       it.name_en = it.name_en;
                       it.name_tw = it.name_tw;
                       it.description = it.description;
                       it.machines = machinelist;
                   }).ToList();


            return list;
        }



    }
}
