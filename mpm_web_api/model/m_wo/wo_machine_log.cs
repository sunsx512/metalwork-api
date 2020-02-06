using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("work_order.wo_machine_log")]
    public class wo_machine_log:base_model
    {
        public int wo_config_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal standard_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime start_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime end_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int quantity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int bad_quantity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal achieving_rate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal productivity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal cycle_time { get; set; }

        public decimal cycle_time_average { get; set; }
    }
}
