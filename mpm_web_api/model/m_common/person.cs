using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("common.person")]
    public class person:base_model
    {

        /// <summary>
        /// 人员名
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// 部门id
        /// </summary>
        public int dept_id { get; set; }
        /// <summary>
        /// 员工卡号
        /// </summary>
        public string id_num { get; set; }
        /// <summary>
        /// 员工等级
        /// </summary>
        public string user_level { get; set; }
        /// <summary>
        /// 邮件
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        public string wechart { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string mobile_phone { get; set; }
        /// <summary>
        /// 员工工位地址
        /// </summary>
        public string user_position { get; set; }
    }
}
