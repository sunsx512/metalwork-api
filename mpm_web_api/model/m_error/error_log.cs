using System;
using SqlSugar;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("andon.error_log")]
    public class error_log:base_model
    {
        /// <summary>
        /// 异常配置id
        /// </summary>
        public int error_config_id { get; set; }
        /// <summary>
        /// 二级标签类型名称
        /// </summary>
        public string tag_type_sub_name { get; set; }
        /// <summary>
        /// 责任人
        /// </summary>
        public string responsible_name { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string machine_name { get; set; }
        /// <summary>
        /// 工单
        /// </summary>
        public string work_order { get; set; }
        /// <summary>
        /// 机种
        /// </summary>
        public string part_number { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? start_time { get; set; }
        /// <summary>
        /// 替代者
        /// </summary>
        public string substitutes { get; set; }
        /// <summary>
        /// 签到时间
        /// </summary>
        public DateTime? arrival_time { get; set; }
        /// <summary>
        /// 异常类型名称
        /// </summary>
        public string error_type_name { get; set; }
        /// <summary>
        /// 详细异常信息
        /// </summary>
        public string error_type_detail_name { get; set; }
        /// <summary>
        /// 解除时间
        /// </summary>
        public DateTime? release_time { get; set; }
        /// <summary>
        /// 不良数量
        /// </summary>
        public decimal defectives_count { set; get; }
        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 花费时间
        /// </summary>
        public decimal? cost_time { get; set; }

    }

    public class error_log_detail : error_log
    {
        /// <summary>
        /// 异常配置信息
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public error_config error_config { set; get; }
    }
}
