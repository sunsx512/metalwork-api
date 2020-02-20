using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_error
{
    [SugarTable("andon.utilization_rate_alert")]
    public class utilization_rate_alert:base_model
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public int machine_id { get; set; }
        /// <summary>
        /// 稼动率类别 0:日稼动率 1:班稼动率 2:工单稼动率
        /// </summary>
        public int utilization_rate_type { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public decimal maximum { set; get; }
        /// <summary>
        /// 最小值
        /// </summary>
        public decimal minimum { set; get; }
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


    public class utilization_rate_alert_detail : utilization_rate_alert
    {
        [SugarColumn(IsIgnore = true)]
        public machine machine { get; set; }
        [SugarColumn(IsIgnore = true)]
        public notification_group notice_group { get; set; }
    }
 }
