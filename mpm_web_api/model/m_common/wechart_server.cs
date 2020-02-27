using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("common.wechart_server")]
    public class wechart_server : base_model
    {
        /// <summary>
        /// 应用名称
        /// </summary>
        public string apply_name { set; get; }
        /// <summary>
        /// 企业id
        /// </summary>
        public string corp_id { get; set; }
        /// <summary>
        /// 应用id
        /// </summary>
        public string apply_agentid { get; set; }
        /// <summary>
        /// 应用密钥
        /// </summary>
        public string apply_secret { get; set; }
        /// <summary>
        /// 应用token
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime create_time { get; set; }
    }
}
