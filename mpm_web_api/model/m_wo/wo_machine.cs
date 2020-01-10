using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("work_order.machine")]
    public class wo_machine
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int area_node_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double standard_time { get; set; }
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
        public int iquantityd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int bad_quantity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double achieving_rate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double productivity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double cycle_time { get; set; }
    }
}
