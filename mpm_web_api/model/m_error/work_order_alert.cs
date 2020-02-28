using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("andon.work_order_alert")]
    public class work_order_alert:base_model
    {
        /// <summary>
        /// 虚拟线设定
        /// </summary>
        public int virtual_line_id { set; get; }
        /// <summary>
        /// 预警类型 0:瓶颈站 1:工单完成 2:逾期未完成  3:超过标准工时
        /// </summary>
        public int alert_type { set; get; }

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


    public class work_order_alert_detail : work_order_alert
    {
        [SugarColumn(IsIgnore = true)]
        public virtual_line virtual_line { get; set; }
        [SugarColumn(IsIgnore = true)]
        public notification_group notice_group { get; set; }
    }
}
