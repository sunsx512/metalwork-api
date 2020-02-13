using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("common.area_node")]
    public class area_node : base_model
    {
        /// <summary>
        /// 英文名称
        /// </summary>
        public string name_en { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>
        public string name_cn { get; set; }
        /// <summary>
        /// 繁体名称
        /// </summary>
        public string name_tw { get; set; }
        /// <summary>
        /// 所在层级id
        /// </summary>
        public int area_layer_id { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 上一级节点的id
        /// </summary>
        public int upper_id { get; set; }

    }

    public class area_node_detail : area_node
    {
        [SugarColumn(IsIgnore = true)]
        public List<area_property> property { get; set; }

    }
}
