using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_common
{
    [SugarTable("common.tag_info_extra")]
    public class tag_info_extra:base_model
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 目标类型 0:machine_id  1:node_id   2:virtual_line_id  3:error_type_detail_id 4:wo_config_id
        /// </summary>
        public int target_type { get; set; }
        /// <summary>
        /// 目标id
        /// </summary>
        public int target_id { get; set; }
        /// <summary>
        /// 二级标签id
        /// </summary>
        public int tag_type_sub_id { get; set; }
    }
}
