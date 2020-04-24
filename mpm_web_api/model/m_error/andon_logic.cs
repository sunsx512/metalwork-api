using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_error
{
    [SugarTable("andon.andon_logic")]
    public class andon_logic:base_model
    {
        /// <summary>
        /// 逻辑名
        /// </summary>
        public string name { set; get; }
        public int level1_notification_group_id { get; set; }
        /// <summary>
        /// 二级通知人员id
        /// </summary>
        public int level2_notification_group_id { get; set; }
        /// <summary>
        /// 三级通知人员id
        /// </summary>
        public int level3_notification_group_id { get; set; }
        /// <summary>
        /// 超时设置
        /// </summary>
        public int timeout_setting { get; set; }
        /// <summary>
        /// 预警形式，个人/群组
        /// </summary>
        public int notice_type { get; set; }
    }

    public class andon_logic_detail : andon_logic
    {
        [SugarColumn(IsIgnore = true)]
        public notification_group level1_notification_group { get; set; }
        [SugarColumn(IsIgnore = true)]
        public notification_group level2_notification_group { get; set; }
        [SugarColumn(IsIgnore = true)]
        public notification_group level3_notification_group { get; set; }
    }
}
