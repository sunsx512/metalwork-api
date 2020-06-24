using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_lpm
{
    [SugarTable("lpm.performance_formula")]
    public class performance_formula:base_model
    {
        /// <summary>
        /// 生产占比
        /// </summary>
        public decimal productivity { set; get; }
        /// <summary>
        /// 出席占比
        /// </summary>
        public decimal attendance { set; get; }
        /// <summary>
        /// 请假占比
        /// </summary>
        public decimal leave { set; get; }
        /// <summary>
        /// 提案占比
        /// </summary>
        public decimal proposal { set; get; }
    }
}
