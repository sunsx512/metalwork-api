﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("work_order.virtual_line_log")]
    public class virtual_line_log:base_model
    {
        /// <summary>
        /// 虚拟线id
        /// </summary>
        public int virtual_line_id { get; set; }
        /// <summary>
        /// 工单配置id
        /// </summary>
        public int wo_config_id { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime start_time { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime end_time { get; set; }
        /// <summary>
        /// 平衡率
        /// </summary>
        public decimal balance_rate { get; set; }
        /// <summary>
        /// 生产率
        /// </summary>
        public decimal productivity { get; set; }
       
    }
}
