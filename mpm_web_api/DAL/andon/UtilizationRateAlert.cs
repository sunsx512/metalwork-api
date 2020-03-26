using mpm_web_api.db;
using mpm_web_api.model;
using mpm_web_api.model.m_error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL.andon
{
    public class UtilizationRateAlert : SqlSugarBase
    {
        public List<utilization_rate_alert_detail> QueryableDetailToList()
        {
            List<utilization_rate_alert_detail> list = DB.Queryable<utilization_rate_alert_detail>()
                                .Mapper((it) =>
                                {
                                    List<machine> machines = DB.Queryable<machine>().Where(x => x.id == it.machine_id).ToList();
                                    List<notification_group> notification_groups = DB.Queryable<notification_group>().Where(x => x.id == it.notice_group_id).ToList();
                                    it.machine = machines.FirstOrDefault();
                                    it.notice_group = notification_groups.FirstOrDefault();
                                }).ToList();
            return list;
        }
    }
}
