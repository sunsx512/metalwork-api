using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("common.email_server")]
    public class email_server : base_model
    {
        /// <summary>
        /// 主机ip
        /// </summary>
        public string host { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public int port { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
    }
}
