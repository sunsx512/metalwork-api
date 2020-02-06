using System;
using SqlSugar;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("andon.error_log")]
    public class error_log:base_model
    {
        /// <summary>
        /// 
        /// </summary>
        public string error_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tag_type_sub_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string responsible_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string machine_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string work_order { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string part_number { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime start_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string substitutes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime arrival_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string error_type_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime release_time { get; set; }
    }
}
