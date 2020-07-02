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
        /// 选项名
        /// </summary>
        public string name { set; get; }
        /// <summary>
        /// 选项占比
        /// </summary>
        public decimal ratio { set; get; }
        /// <summary>
        /// 其否启用
        /// </summary>
        public bool enable { set; get; }
    }
}
