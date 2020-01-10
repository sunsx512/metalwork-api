using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("common.wechart_server")]
    public class wechart_server
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string apply_name { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string corp_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string apply_agentid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string apply_secret { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string access_token { get; set; }
    }
}
