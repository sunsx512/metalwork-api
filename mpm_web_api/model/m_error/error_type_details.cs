using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace mpm_web_api.model
{
    [SugarTable("andon.error_type_details")]
    public class error_type_details
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// id
        /// </summary>
        public int error_type_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
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
        public string description { get; set; }
    }
}
