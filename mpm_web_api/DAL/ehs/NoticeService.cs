using mpm_web_api.db;
using mpm_web_api.model;
using mpm_web_api.model.m_ehs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL.ehs
{
    public class NoticeService : SqlSugarBase
    {
        public List<notice_logic_detail> GetNoticeLogicDetail()
        {
            List<notice_logic_detail> list = DB.Queryable<notice_logic_detail>().Mapper((it) =>
            {
                List<notification_group> notification_Groups = DB.Queryable<notification_group>().ToList();
                it.id = it.id;
                it.name = it.name;
                it.notice_type = it.notice_type;
                it.normal_notification_group = notification_Groups.Where(x => x.id == it.normal_notification_group_id).FirstOrDefault();
                it.abnormal_notification_group = notification_Groups.Where(x => x.id == it.abnormal_notification_group_id).FirstOrDefault();
                it.serious_notification_group = notification_Groups.Where(x => x.id == it.serious_notification_group_id).FirstOrDefault();
            }).OrderBy(x => x.id).ToList();
            return list;
        }
    }
}
