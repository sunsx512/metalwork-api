using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("work_order.virtual_line")]
    public class virtual_line
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int wo_config_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string station_list { get; set; }
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
        public double balance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
    }
}
