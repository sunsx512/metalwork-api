using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_error
{
    [SugarTable("andon.capacity_alert")]
    public class capacity_alert:base_model
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime date { set; get; }
        /// <summary>
        /// 标准产能
        /// </summary>
        public decimal capacity { set; get; }
        /// <summary>
        /// 预警的方式 0:微信  1:邮件  2:邮件&微信
        /// </summary>
        public int notice_type { set; get; }
        /// <summary>
        /// 通知群组的id
        /// </summary>
        public int notice_group_id { set; get; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool enable { set; get; }
    }

    public class capacity_alert_detail : capacity_alert
    {
        [SugarColumn(IsIgnore = true)]
        public notification_group notice_group { get; set; }
    }
}
