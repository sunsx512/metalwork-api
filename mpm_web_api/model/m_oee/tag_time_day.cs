using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;
namespace mpm_web_api.model
{
    public class tag_time_day
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
        public DateTime date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int duration_time { get; set; }
    }
}
