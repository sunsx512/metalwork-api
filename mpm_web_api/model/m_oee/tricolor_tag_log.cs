using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;
namespace mpm_web_api.model
{
    [SugarTable("oee.tricolor_tag_log")]
    public class tricolor_tag_log
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int machine_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string status_name { get; set; }
   
        /// <summary>
        /// 
        /// </summary>
        public DateTime insert_time { get; set; }
    }
}
