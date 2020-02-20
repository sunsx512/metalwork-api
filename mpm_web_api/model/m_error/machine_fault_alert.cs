using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_error
{
    [SugarTable("andon.machine_fault_alert")]
    public class machine_fault_alert:base_model
    {
        /// <summary>
        /// 详细故障原因id
        /// </summary>
        public int error_type_detail_id { set; get; }
        /// <summary>
        /// 预警故障次数/天
        /// </summary>
        public int fault_times { set; get; }
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

    public class machine_fault_alert_detail : machine_fault_alert
    {
        [SugarColumn(IsIgnore = true)]
        public error_type error_type { get; set; }
        [SugarColumn(IsIgnore = true)]
        public error_type_details error_type_detail { get; set; }
        [SugarColumn(IsIgnore = true)]
        public notification_group notice_group { get; set; }
    }
}
