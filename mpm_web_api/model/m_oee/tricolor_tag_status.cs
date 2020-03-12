using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_oee
{
    [SugarTable("oee.tricolor_tag_status")]
    public class tricolor_tag_status:base_model
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public int machine_id { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string machine_name { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        public string status_name { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime insert_time { get; set; }
        /// <summary>
        /// 预留
        /// </summary>
        public int whether { get; set; }
    }
}
