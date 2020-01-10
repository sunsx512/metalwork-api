using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("work_order.process")]
    public class process
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double standard_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int wo_config_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int process_config_id { get; set; }
    }
}
