using mpm_web_api.db;
using mpm_web_api.model;
using mpm_web_api.model.m_error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL.andon
{
    public class QualityAlertService : SqlSugarBase
    {
        public List<quality_alert_detail> QueryableDetailToList()
        {
            List<quality_alert_detail> list = DB.Queryable<quality_alert_detail>()
                                .Mapper((it) =>
                                {
                                    List<wo_config> wo_configs = DB.Queryable<wo_config>().Where(x => x.id == it.work_order_id).ToList();
                                    List<notification_group> notification_groups = DB.Queryable<notification_group>().Where(x => x.id == it.notice_group_id).ToList();
                                    it.notice_group = notification_groups.First();
                                    it.work_order = wo_configs.First();
                                }).ToList();
            return list;
        }
    }
}
