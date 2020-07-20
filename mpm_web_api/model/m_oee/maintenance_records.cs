using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_oee
{
    [SugarTable("oee.maintenance_records")]
    public class maintenance_records:base_model
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public int machine_id { set; get; }
        /// <summary>
        /// 保养日期
        /// </summary>
        public DateTime time { set; get; }
        /// <summary>
        /// 描述
        /// </summary>
        public string description { set; get; }
    }
}
