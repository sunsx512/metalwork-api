using mpm_web_api.db;
using mpm_web_api.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL.andon
{
    public class WorkOrderAlertService : SqlSugarBase
    {

        public List<work_order_alert_detail> QueryableDetailToList()
        {
            List<work_order_alert_detail> list = DB.Queryable<work_order_alert_detail>()
                                .Mapper((it) =>
                                {
                                    List<virtual_line> virtual_lines = DB.Queryable<virtual_line>().Where(x => x.id == it.virtual_line_id).ToList();
                                    List<notification_group> notification_groups = DB.Queryable<notification_group>().Where(x => x.id == it.notice_group_id).ToList();
                                    it.virtual_line = virtual_lines.FirstOrDefault();
                                    it.notice_group = notification_groups.FirstOrDefault();
                                }).OrderBy(x=>x.id).ToList();
            return list;
        }
    }
}
