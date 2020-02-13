using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_oee
{
    public class machine_status_duration_alert:base_model
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public int machine_id { set; get; }
        /// <summary>
        /// 设备状态
        /// </summary>
        public string machine_status { set; get; }
        /// <summary>
        /// 持续时间  超出持续时间则会触发警报
        /// </summary>
        public Decimal duration { set; get; }
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
