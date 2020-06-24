using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_lpm
{
    [SugarTable("lpm.overtime_statistics")]
    public class overtime_statistics:base_model
    {
        /// <summary>
        /// 人员id
        /// </summary>
        public int person_id { set; get; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime start_time { set; get; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime end_time { set; get; }
        /// <summary>
        /// 总计时长
        /// </summary>
        public decimal duration { set; get; }
    }
}
