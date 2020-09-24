using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_common
{
    public class raw_data
    {
        /// <summary>
        /// 点名称
        /// </summary>
        public string name { set; get; }
        /// <summary>
        /// 点位类型
        /// </summary>
        public string tag_type_sub { set; get; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string machine_name { set; get; }
        /// <summary>
        /// 值
        /// </summary>
        public string value { set; get; }
        /// <summary>
        /// 插入时间
        /// </summary>
        public DateTime ts { set; get; }
    }
}
