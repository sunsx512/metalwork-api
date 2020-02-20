using mpm_web_api.model;
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
                List<notification_group> level1_notification_group = DB.Queryable<notification_group>().Where(x => x.id == it.level1_notification_group_id).ToList();
                List<notification_group> level2_notification_group = DB.Queryable<notification_group>().Where(x => x.id == it.level2_notification_group_id).ToList();
                List<notification_group> level3_notification_group = DB.Queryable<notification_group>().Where(x => x.id == it.level3_notification_group_id).ToList();
                it.id = it.id;
                it.alert_active = it.alert_active;
                it.level1_notification_group = level1_notification_group.First();
                it.level2_notification_group = level2_notification_group.First();
                it.level3_notification_group = level3_notification_group.First();
                it.machine = machines.First();
                it.notice_type = it.notice_type;
                it.response_person = persons.First();
                it.response_person_id = it.response_person_id;
                it.timeout_setting = it.timeout_setting;
                it.trigger_out_color = it.trigger_out_color;
                it.type = types.First();
            }).ToList();
            return list;
        }
    }
}
