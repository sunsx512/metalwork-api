using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("work_order.wo_machine")]
    public class wo_machine:base_model
    {
        public int virtual_line_id { get; set; }
        public int machine_id { get; set; }

    }
    [SugarTable("work_order.wo_machine_detail")]
    public class wo_machine_detail : wo_machine
    {
        public string name_cn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name_en { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name_tw { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int area_node_id { get; set; }

    }
}
