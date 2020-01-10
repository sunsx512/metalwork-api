using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;
namespace mpm_web_api.model
{
    [SugarTable("oee.utilization_rate_shift")]
    public class utilization_rate_shift
    {
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int machine_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shift { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal utilization_rate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime insert_time { get; set; }
    }
}
