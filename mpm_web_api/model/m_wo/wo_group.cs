using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("work_order.wo_group")]
    public class wo_group
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int process_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int station_id { get; set; }
    }
}
