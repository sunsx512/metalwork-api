using mpm_web_api.model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL
{
    public class NotificationGroupService : BaseService<notification_group>
    {
        public new List<notification_group_detail> QueryableToList()
        {
            var list = DB.Queryable<notification_group_detail>()
            .Mapper((it) =>
            {               
                List<notification_person_detail> notification_persons = DB.Queryable<notification_person_detail>().Where(x=>x.notification_group_id == it.id).ToList();               
                it.id = it.id;
                it.name_cn = it.name_cn;
                it.name_en = it.name_en;
                it.name_tw = it.name_tw;
                it.person = notification_persons;
            }).ToList();
            return list;
        }


    }
}
