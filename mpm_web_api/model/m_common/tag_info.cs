using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("common.tag_info")]
    public class tag_info:base_model
    {
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int machine_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int tag_type_sub_id { get; set; }
    }

    public class tag_info_detail : tag_info
    {
        [SugarColumn(IsIgnore = true)]
        public tag_type tag_type { get; set; }
        [SugarColumn(IsIgnore = true)]
        public tag_type_sub tag_type_sub { get; set; }
        [SugarColumn(IsIgnore = true)]
        public machine machine { set; get; }
    }

}
