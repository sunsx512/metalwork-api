using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_ehs
{
    [SugarTable("ehs.notice_logic")]
    public class notice_logic:base_model
    {
        /// <summary>
        /// 通知名
        /// </summary>
        public string name { set; get; }
        /// <summary>
        /// 正常通知群组id
        /// </summary>
        public int normal_notification_group_id { get; set; }
        /// <summary>
        /// 异常通知人员id
        /// </summary>
        public int abnormal_notification_group_id { get; set; }
        /// <summary>
        /// 严重通知人员id
        /// </summary>
        public int serious_notification_group_id { get; set; }
        /// <summary>
        /// 预警形式，微信，邮件，微信&邮件
        /// </summary>
        public int notice_type { get; set; }
    }
    public class notice_logic_detail : notice_logic
    {
        [SugarColumn(IsIgnore = true)]
        /// <summary>
        /// 正常通知群组
        /// </summary>
        public notification_group normal_notification_group { set; get; }
        [SugarColumn(IsIgnore = true)]
        /// <summary>
        /// 异常通知群组
        /// </summary>
        public notification_group abnormal_notification_group { set; get; }
        [SugarColumn(IsIgnore = true)]
        /// <summary>
        /// 严重通知群组
        /// </summary>
        public notification_group serious_notification_group { set; get; }
    }
}
