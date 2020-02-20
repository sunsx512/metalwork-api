using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_error
{
    [SugarTable("andon.quality_alert")]
    public class quality_alert:base_model
    {
        /// <summary>
        /// 工单号
        /// </summary>
        public int work_order_id { set; get; }
        /// <summary>
        /// 预警不良数量
        /// </summary>
        public int defective_number { set; get; }
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

    public class quality_alert_detail : quality_alert
    {
        [SugarColumn(IsIgnore = true)]
        public wo_config work_order { get; set; }
        [SugarColumn(IsIgnore = true)]
        public notification_group notice_group { get; set; }
    }
}
