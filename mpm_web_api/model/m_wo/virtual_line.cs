using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("work_order.virtual_line")]
    public class virtual_line:base_model
    {
        /// <summary>
        /// 中文名称
        /// </summary>
        public string name_cn { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string name_en { get; set; }
        /// <summary>
        /// 繁体名称
        /// </summary>
        public string name_tw { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }
        
    }

    public class virtual_line_detail : virtual_line
    {
        /// <summary>
        /// 设备列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<wo_machine_detail> machines { get; set; }

    }
}
