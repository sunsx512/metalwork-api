using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_wo
{
    [SugarTable("work_order.capacity_config")]
    public class capacity_config
    {
        [SugarColumn(IsIdentity = true, ColumnName = "id")]
        public int id { set; get; }
        [SugarColumn(IsPrimaryKey = true, ColumnName = "date")]
        //日期
        public DateTime date { set; get;}
        //标准产能
        public decimal capacity { set; get;}
        /// <summary>
        /// 利用率 计算得到
        /// </summary>
        public decimal utilization { set; get; }
    }
}
