using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("andon.notification_person")]
    public class notification_person:base_model
    {
        /// <summary>
        /// 人员id
        /// </summary>
        public int person_id { get; set; }
        /// <summary>
        /// 通知群组id
        /// </summary>
        public int notification_group_id { get; set; }
    }
    [SugarTable("andon.notification_person_detail")]
    public class notification_person_detail : notification_person
    {
        public string user_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int dept_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string id_num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string user_level { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string wechart { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mobile_phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string user_position { get; set; }
    }
}
