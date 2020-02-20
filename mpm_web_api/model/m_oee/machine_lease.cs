using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_oee
{
    [SugarTable("oee.machine_lease")]
    public class machine_lease:base_model
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public int machine_id { get; set; }
        /// <summary>
        /// 租赁单价  每小时
        /// </summary>
        public decimal unit_price { get; set; }
        /// <summary>
        /// 0:按月计费模式  1:余额计费模式
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 账户存款
        /// </summary>
        public decimal total_price { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime start_time { get; set; }
    }
    public class machine_lease_detail: machine_lease
    {
        [SugarColumn(IsIgnore = true)]
        public machine machine { get; set; }
    }
}
