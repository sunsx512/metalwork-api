using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("work_order.wo_config")]
    public class wo_config
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string work_order { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string part_num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shift { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int standard_num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool auto { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int order_index { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }

    }
}
