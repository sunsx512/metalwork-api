using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_wo
{
    [SugarTable("work_order.overdue_work_order")]
    public class overdue_work_order:base_model
    {
        /// <summary>
        /// 开始时间 结束时间
        /// </summary>
        public DateTime? start_time { set; get; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? end_time { set; get; }
        /// <summary>
        /// 逾期时间
        /// </summary>
        public decimal overdue_time { set; get; }
        /// <summary>
        /// 工单id
        /// </summary>
        public int wo_config_id { set; get; }
        /// <summary>
        /// 责任单位
        /// </summary>
        public string responsible_unit { set; get; }
        /// <summary>
        /// 逾期原因
        /// </summary>
        public string reason { set; get; }

    }
}
