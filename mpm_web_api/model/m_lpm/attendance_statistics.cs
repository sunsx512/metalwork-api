using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_lpm
{
    [SugarTable("lpm.attendance_statistics")]
    public class attendance_statistics:base_model
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? date { set; get; }
        /// <summary>
        /// 白班
        /// </summary>
        public int shift { set; get; }
        /// <summary>
        /// 人员id
        /// </summary>
        public int person_id { set; get; }
        /// <summary>
        /// 是否出席
        /// </summary>
        public bool is_attend { set; get; }
    }
}
