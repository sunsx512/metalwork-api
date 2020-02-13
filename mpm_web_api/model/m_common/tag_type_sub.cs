using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("common.tag_type_sub")]
    public class tag_type_sub:base_model
    {
        /// <summary>
        /// 中文名称
        /// </summary>
        public string name_cn { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string name_en { get; set; }
        /// <summary>
        /// 繁体名称
        /// </summary>
        public string name_tw { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 一级标签类型id
        /// </summary>
        public int tag_type_id { get; set; }
    }
}
