using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_error
{
    [SugarTable("andon.machine_cost_alert")]
    public class machine_cost_alert:base_model
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public int machine_id { get; set; }
        /// <summary>
        /// 在月费用超支预警模式下为最大月费用  在余额不足预警模式下为最小余额数
        /// </summary>
        public decimal cost { set; get; }
        /// <summary>
        /// 预警模式  0:月费用超支预警  1:余额不足预警
        /// </summary>
        public int alert_mode { set; get; }
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

    public class machine_cost_alert_detail : machine_cost_alert
    {
        [SugarColumn(IsIgnore = true)]
        public machine machine { get; set; }
        [SugarColumn(IsIgnore = true)]
        public notification_group notice_group { get; set; }
    }
}
