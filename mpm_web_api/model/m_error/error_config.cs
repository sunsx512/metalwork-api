using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;
namespace mpm_web_api.model
{
    [SugarTable("andon.error_config")]
    public class error_config
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 设备id
        /// </summary>
        public int machine_id { get; set; }
        /// <summary>
        /// 工序id
        /// </summary>
        public int process_config_id { get; set; }
        /// <summary>
        /// 标签编码id
        /// </summary>
        public int tag_type_sub_id { get; set; }
        /// <summary>
        /// 功能是否激活
        /// </summary>
        public bool alert_active { get; set; }
        /// <summary>
        /// 异常灯颜色
        /// </summary>
        public int trigger_out_color { get; set; }
        /// <summary>
        /// 预警方式，微信邮箱三色灯语音
        /// </summary>
        public bool group_delivery { get; set; }
        /// <summary>
        /// 超时设置
        /// </summary>
        public int timeout_setting { get; set; }
        /// <summary>
        /// 预警形式，个人/群组
        /// </summary>
        public int notice_type { get; set; }
    
    }
}
