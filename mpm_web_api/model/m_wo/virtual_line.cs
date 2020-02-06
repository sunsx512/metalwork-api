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
        public string name_cn { get; set; }
        public string name_en { get; set; }
        public string name_tw { get; set; }
        public string description { get; set; }
        
    }

    public class virtual_line_detail : virtual_line
    {
        [SugarColumn(IsIgnore = true)]
        public List<wo_machine_detail> machines { get; set; }

    }
}
