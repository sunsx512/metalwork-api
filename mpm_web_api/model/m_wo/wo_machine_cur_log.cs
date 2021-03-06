using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_wo
{
    [SugarTable("work_order.wo_machine_current_log")]
    public class wo_machine_cur_log: base_model
    {
        /// <summary>
        /// 工单id
        /// </summary>
        public int wo_config_id { get; set; }
        /// <summary>
        /// 标准数量
        /// </summary>
        public decimal standard_time { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime start_time { get; set; }
        /// <summary>
        /// 当前数量
        /// </summary>
        public decimal quantity { get; set; }
        /// <summary>
        /// 不良数量
        /// </summary>
        public decimal bad_quantity { get; set; }
        /// <summary>
        /// 达成率
        /// </summary>
        public decimal achieving_rate { get; set; }
        /// <summary>
        /// 生产率
        /// </summary>
        public decimal productivity { get; set; }
        /// <summary>
        /// 生产效率
        /// </summary>
        public decimal production_efficiency { set; get; }
        /// <summary>
        /// 当前C/T
        /// </summary>
        public decimal cycle_time { get; set; }
        /// <summary>
        /// 平均C/T
        /// </summary>
        public decimal cycle_time_average { get; set; }
        /// <summary>
        /// 设备id
        /// </summary>
        public decimal machine_id { get; set; }
        /// <summary>
        /// 标准数量
        /// </summary>
        public int standard_num { get; set; }
        
    }
    public class wo_machine_cur_log_detail : wo_machine_cur_log
    {
        /// <summary>
        /// 工单信息
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public wo_config work_order { get; set; }
    }
}
