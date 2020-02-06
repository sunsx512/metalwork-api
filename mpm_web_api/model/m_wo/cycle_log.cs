using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("work_order.cycle_current")]
    public class cycle_log:base_model
    {

        /// <summary>
        /// 
        /// </summary>
        public int wo_machine_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime insert_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double value { get; set; }
       
    }
}
