using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("work_order.wo_config")]
    public class wo_config:base_model
    {
        public string work_order { get; set; }
        public string part_num { get; set; }
        public string shift { get; set; }
        public int standard_num { get; set; }
        public bool auto { get; set; }
        public int order_index { get; set; }
        public int status { get; set; }
        public int virtual_line_id { get; set; }
        public string standard_time { get; set; }
        
    }

    public class wo_config_detail : wo_config
    {
        [SugarColumn(IsIgnore = true)]
        public virtual_line virtual_Line {set; get;}

    }


}
