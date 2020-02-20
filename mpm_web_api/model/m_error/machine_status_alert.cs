using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_error
{
    [SugarTable("andon.machine_status_alert")]
    public class machine_status_alert:base_model
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public int machine_id { get; set; }
        /// <summary>
        /// 设备状态
        /// </summary>
        public string machine_status { set; get; }
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

    public class machine_status_alert_detail : machine_status_alert
    {
        [SugarColumn(IsIgnore = true)]
        public machine machine { get; set; }
        [SugarColumn(IsIgnore = true)]
        public notification_group notice_group { get; set; }
    }
}
