using mpm_web_api.db;
using mpm_web_api.model;
using mpm_web_api.model.m_error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL.andon
{
    public class AndonLogicService: SqlSugarBase
    {
        public List<andon_logic_detail> QueryableDetailToList()
        {
            var list = DB.Queryable<andon_logic_detail>()
            .Mapper((it) =>
            {
                andon_logic al = DB.Queryable<andon_logic>().Where(x=>x.id == it.id).First();
                List<notification_group> notification_Groups = DB.Queryable<notification_group>().ToList();
                it.id = it.id;
                it.name = al.name;
                it.notice_type = al.notice_type;
                it.timeout_setting = al.timeout_setting;
                it.level1_notification_group = notification_Groups.Where(x=>x.id == al.level1_notification_group_id).FirstOrDefault();
                it.level2_notification_group = notification_Groups.Where(x => x.id == al.level2_notification_group_id).FirstOrDefault();
                it.level3_notification_group = notification_Groups.Where(x => x.id == al.level3_notification_group_id).FirstOrDefault();
            }).ToList();
            return list;
        }

    }
}
