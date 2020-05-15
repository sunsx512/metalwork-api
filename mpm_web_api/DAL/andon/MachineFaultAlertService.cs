using mpm_web_api.db;
using mpm_web_api.model;
using mpm_web_api.model.m_error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL.andon
{
    public class MachineFaultAlertService: SqlSugarBase
    {
        public List<machine_fault_alert_detail> QueryableDetailToList()
        {
            List<machine_fault_alert_detail> list = DB.Queryable<machine_fault_alert_detail>()
                                .Mapper((it) =>
                                {
                                    List<error_type_details> error_type_details = DB.Queryable<error_type_details>().Where(x => x.id == it.error_type_detail_id).ToList();
                                    List<error_type> error_types = DB.Queryable<error_type>().Where(x => x.id == error_type_details.First().error_type_id).ToList();
                                    List<notification_group> notification_groups = DB.Queryable<notification_group>().Where(x => x.id == it.notice_group_id).ToList();
                                    it.notice_group = notification_groups.FirstOrDefault();
                                    it.error_type = error_types.FirstOrDefault();
                                    it.error_type_detail = error_type_details.FirstOrDefault();
                                }).OrderBy(x=>x.id).ToList();
            return list;
        }
    }
}
