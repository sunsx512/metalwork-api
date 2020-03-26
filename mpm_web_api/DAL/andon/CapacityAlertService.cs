using mpm_web_api.db;
using mpm_web_api.model;
using mpm_web_api.model.m_error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL.andon
{
    public class CapacityAlertService: SqlSugarBase
    {
        public List<capacity_alert_detail> QueryableDetailToList()
        {
            List<capacity_alert_detail> list = DB.Queryable<capacity_alert_detail>()
                                .Mapper((it) =>
                                {
                                    List<notification_group> notification_groups = DB.Queryable<notification_group>().Where(x => x.id == it.notice_group_id).ToList();                                   
                                    it.notice_group = notification_groups.FirstOrDefault();
                                }).ToList();
            return list;
        }
    }
}
