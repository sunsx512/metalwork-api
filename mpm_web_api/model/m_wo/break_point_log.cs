using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_wo
{
    [SugarTable("work_order.breakpoint_log")]
    public class break_point_log:base_model
    {
        public DateTime insert_time { set; get; }
        /// <summary>
        /// 设备id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public int machine_id { set; get; }
        /// <summary>
        /// 主工单id
        /// </summary>
        public int work_order_id { set; get; }
        /// <summary>
        /// 断点类型 0: 无节拍 1:半个节拍
        /// </summary>
        public int breakpoint { set; get; }
    }
}
