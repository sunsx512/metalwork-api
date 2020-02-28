using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("work_order.wo_machine_log")]
    public class wo_machine_log:base_model
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
        /// 结束时间
        /// </summary>
        public DateTime end_time { get; set; }
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
}
