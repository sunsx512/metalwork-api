using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;
namespace mpm_web_api.model
{
    [SugarTable("andon.error_config")]
    public class error_config:base_model
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public int machine_id { get; set; }
        /// <summary>
        /// 标签编码id
        /// </summary>
        public int tag_type_sub_id { get; set; }
        /// <summary>
        /// 责任人员id
        /// </summary>
        public int response_person_id { get; set; }
        /// <summary>
        /// 一级通知人员id
        /// </summary>
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
        /// 功能是否激活
        /// </summary>
        public bool alert_active { get; set; }
        /// <summary>
        /// 异常灯颜色
        /// </summary>
        public int trigger_out_color { get; set; }
        /// <summary>
        /// 超时设置
        /// </summary>
        public int timeout_setting { get; set; }
        /// <summary>
        /// 预警形式，个人/群组
        /// </summary>
        public int notice_type { get; set; }
        /// <summary>
        /// 逻辑类型 0:安灯逻辑 1:自定义逻辑
        /// </summary>
        public int  logic_type{ get; set; }
        
    }
    public class error_config_detail : error_config
    {
        [SugarColumn(IsIgnore = true)]
        public machine machine { get; set; }
        [SugarColumn(IsIgnore = true)]
        public tag_type_sub type { get; set; }
        [SugarColumn(IsIgnore = true)]
        public person response_person { get; set; }
        [SugarColumn(IsIgnore = true)]
        public notification_group level1_notification_group { get; set; }
        [SugarColumn(IsIgnore = true)]
        public notification_group level2_notification_group { get; set; }
        [SugarColumn(IsIgnore = true)]
        public notification_group level3_notification_group { get; set; }
    }
 }
