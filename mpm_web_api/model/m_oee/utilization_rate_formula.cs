using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;


namespace mpm_web_api.model
{
    [SugarTable("oee.utilization_rate_formula")]
    public class utilization_rate_formula:base_model
    {
        /// <summary>
        /// 
        /// </summary>
        public string formula { get; set; }
      
    }
}
