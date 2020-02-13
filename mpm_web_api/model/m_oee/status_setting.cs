using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace mpm_web_api.model
{
    [SugarTable("oee.status_setting")]
    public class status_setting:base_model
    {

        public int value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string status_name { get; set; }
    }
}
