using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace mpm_web_api.model
{
    [SugarTable("andon.material_request_info")]
    public class material_request_info:base_model
    {
        /// <summary>
        /// 物料名称
        /// </summary>
        public string material_code { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string machine_name { get; set; }
        /// <summary>
        /// 请求人员名
        /// </summary>
        public string request_person_name { get; set; }
        /// <summary>
        /// 工单号
        /// </summary>
        public string work_order { get; set; }
        /// <summary>
        /// 机种号
        /// </summary>
        public string part_number { get; set; }
        /// <summary>
        /// 请求数量
        /// </summary>
        public int request_count { get; set; }
        /// <summary>
        /// 送料人员名
        /// </summary>
        public string take_person_name { get; set; }
        /// <summary>
        /// 花费时间
        /// </summary>
        public DateTime take_time { get; set; }
        /// <summary>
        /// 触发时间
        /// </summary>
        public DateTime createtime { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 异常配置id
        /// </summary>
        public int error_config_id { get; set; }
    }
}
