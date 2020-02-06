using mpm_web_api.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL
{
    public class WoConfigService: BaseService<wo_config>
    {
        public new List<wo_config_detail> QueryableToList()
        {
            var list = DB.Queryable<wo_config_detail>()
                   .Mapper((it) =>
                   {
                       var virtual_line = DB.Queryable<virtual_line>().Where(x => x.id == it.virtual_line_id).ToList();
                       it.id = it.id;
                       it.auto = it.auto;
                       it.order_index = it.order_index;
                       it.part_num = it.part_num;
                       it.shift = it.shift;
                       it.standard_num = it.standard_num;
                       it.standard_time = it.standard_time;
                       it.status = it.status;
                       it.virtual_Line = virtual_line.First();
                       it.virtual_line_id = it.virtual_line_id;
                       it.work_order = it.work_order;
                   }).ToList();
            return list;
        }

    }
}
