using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("work_order.virtual_line_log")]
    public class virtual_line_log:base_model
    {
        public int virtual_line_id { get; set; }
        public int wo_config_id { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public decimal balance_rate { get; set; }


    }
}
