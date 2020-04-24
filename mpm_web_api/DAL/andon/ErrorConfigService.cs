using mpm_web_api.model;
using mpm_web_api.model.m_error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL.andon
{
    public class ErrorConfigService : BaseService<wo_config_detail>
    {
        public new List<error_config_detail> QueryableToList()
        {
            var list = DB.Queryable<error_config_detail>()
            .Mapper((it) =>
            {
                List<machine> machines = DB.Queryable<machine>().Where(x => x.id == it.machine_id).ToList();
                List<tag_type_sub> types = DB.Queryable<tag_type_sub>().Where(x => x.id == it.tag_type_sub_id).ToList();
                List<person> persons = DB.Queryable<person>().Where(x => x.id == it.response_person_id).ToList();
                List<tag_info> tag = DB.Queryable<tag_info>().Where(x => x.machine_id == it.machine_id && x.tag_type_sub_id == it.tag_type_sub_id).ToList();
                List<andon_logic> andon_logic = DB.Queryable<andon_logic>().Where(x => x.id == it.andon_logic_id).ToList();
                it.id = it.id;
                it.alert_active = it.alert_active;
                it.machine = machines.FirstOrDefault();
                it.tag = tag.FirstOrDefault();
                it.response_person = persons.FirstOrDefault();
                it.response_person_id = it.response_person_id;
                it.trigger_out_color = it.trigger_out_color;
                it.andon_logic = andon_logic.FirstOrDefault();
                it.type = types.FirstOrDefault();
            }).ToList();
            return list;
        }
    }
}
