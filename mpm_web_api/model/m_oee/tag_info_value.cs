using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_oee
{
    [SugarTable("oee.tag_info_value")]
    public class tag_info_value
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        public int id { get; set; }
        /// <summary>
        /// 标签点
        /// </summary>
        public int tag_info_id { set; get; }
        /// <summary>
        /// 值
        /// </summary>
        public string value { set; get; }
        /// <summary>
        /// 更新的时间
        /// </summary>
        public DateTime update_time { set; get; }
        /// <summary>
        /// 标签名
        /// </summary>
        public string tag_name { set; get; }
    }
}
