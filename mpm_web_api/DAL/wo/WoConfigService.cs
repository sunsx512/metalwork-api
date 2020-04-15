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
                       it.virtual_Line = virtual_line.FirstOrDefault();
                       it.virtual_line_id = it.virtual_line_id;
                       it.work_order = it.work_order;
                   }).ToList();
            return list;
        }

        public List<wo_config> QueryableByMachine(int machine_id)
        {
            List<wo_config> list = new List<wo_config>();
            var machines = DB.Queryable<wo_machine_detail>().Where(x => x.machine_id == machine_id).ToList();
            if(machines.Count > 0)
            {
                var virtual_line = DB.Queryable<virtual_line>().Where(x => x.id == machines.First().virtual_line_id).ToList();
                if(virtual_line.Count >0)
                {
                    list = DB.Queryable<wo_config>().Where(x => x.virtual_line_id == virtual_line.First().id).ToList();
                }
            }
            return list;
        }

        public  List<wo_config_excute> QueryableByStatus(int status)
        {
            var list = DB.Queryable<wo_config_excute>()
                   .Where(x=>x.status >= status)
                   .Mapper((it) =>
                   {
                       List<virtual_line_log> virtual_line_log = DB.Queryable<virtual_line_log>().Where(x => x.wo_config_id == it.id).ToList();
                       if(virtual_line_log.Count > 0)
                       {
                           var virtual_line = DB.Queryable<virtual_line>().Where(x => x.id == virtual_line_log.First().virtual_line_id).ToList();
                           it.id = it.id;
                           it.auto = it.auto;
                           it.create_time = it.create_time;
                           it.order_index = it.order_index;
                           it.part_num = it.part_num;
                           it.shift = it.shift;
                           it.standard_num = it.standard_num;
                           it.standard_time = it.standard_time;
                           it.status = it.status;
                           it.virtual_line_id = it.virtual_line_id;
                           it.virtual_Line_log = virtual_line_log.FirstOrDefault();
                           it.virtual_Line = virtual_line.FirstOrDefault();
                           it.work_order = it.work_order;
                       }
                   }).ToList();
            return list;
        }


        public bool InserWorkOrder(wo_config wo)
        {
            string str = "";
            List<wo_machine> list = DB.Queryable<wo_machine>()
                         .Where(x => x.virtual_line_id == wo.virtual_line_id).ToList();
            if(list.Count > 0)
            {
                foreach(wo_machine wm in list)
                {
                    str += wm.machine_id.ToString() + ";";
                }
            }
            //删除最后一位
            wo.lbr_formula = str.Remove(str.Length - 1, 1);
            return DB.Insertable(wo).ExecuteCommand() > 0;
        }

    }
}
