using mpm_web_api.model.m_wo;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("work_order.wo_machine_log")]
    public class wo_machine_log: wo_machine_cur_log
    {
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime end_time { get; set; }
    }
}
