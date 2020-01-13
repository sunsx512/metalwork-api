using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;


namespace mpm_web_api.model
{
    [SugarTable("oee.utilization_rate_formula")]
    public class utilization_rate_formula
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string numerator { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string denominator { get; set; }
    }
}
