using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("common.area_node")]
    public class area_node : base_model
    {
        /// <summary>
        /// 
        /// </summary>
        public string name_en { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name_cn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name_tw { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int area_layer_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int upper_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string property { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int property_id { get; set; }
    }
}
