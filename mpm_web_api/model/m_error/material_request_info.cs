using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace mpm_web_api.model
{
    [SugarTable("andon.material_request_info")]
    public class material_request_info
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string material_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string machine_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string request_person_name { get; set; }
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
        public int request_count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string take_person_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime take_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime createtime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
    }
}
