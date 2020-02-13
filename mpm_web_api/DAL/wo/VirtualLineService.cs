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

        public bool DeleteByMachine(int virtual_line_id, int machine_id)
        {
            return DB.Deleteable<wo_machine>()
                .Where(x => x.machine_id == machine_id)
                .Where(x => x.virtual_line_id == virtual_line_id).ExecuteCommand() > 0;
        }

    }
}
