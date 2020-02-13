using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_oee
{
    public class utilization_rate_alert:base_model
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public int machine_id { set; get; }
        /// <summary>
        /// 稼动率类别 日稼动率  班别稼动率 工单稼动率
        /// </summary>
        public string utilization_rate_type { set; get; }
        /// <summary>
        /// 最小值
        /// </summary>
        public Decimal maximum { set; get; }
        /// <summary>
        /// 最大值
        /// </summary>
        public Decimal minimum { set; get; }
        /// <summary>
        /// 通知类型 0:微信 1:邮件 2:微信&邮件
        /// </summary>
        public int notice_type { set; get; }
        /// <summary>
        /// 通知人员id
        /// </summary>
        public int notice_person_id { set; get; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool enable { set; get; }
    }
}
