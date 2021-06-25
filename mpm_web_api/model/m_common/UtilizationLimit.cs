using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_common
{
    [SugarTable("common.utilization_limit")]
    public class UtilizationLimit:base_model
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public int machine_id { set; get; }
        /// <summary>
        /// 开始计算的时间
        /// </summary>
        public DateTime start_time { set; get; }
        /// <summary>
        /// 结束计算的时间
        /// </summary>
        public DateTime end_time { set; get; }
    }
}
