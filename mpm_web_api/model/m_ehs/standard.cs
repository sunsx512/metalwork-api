using MongoDB.Driver;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_ehs
{
    [SugarTable("ehs.standard")]
    public class standard:base_model
    {
       /// <summary>
       /// 标签id
       /// </summary>
       public int tag_id { set; get; }
        /// <summary>
        /// 标签类型id
        /// </summary>
       public int tag_type_sub_id { set; get; }
        /// <summary>
        /// 普通范围最小值
        /// </summary>
       public decimal? normal_min { set; get; }
        /// <summary>
        /// 普通范围最大值
        /// </summary>
       public decimal? normal_max { set; get; }
        /// <summary>
        /// 异常范围最小值
        /// </summary>
       public decimal? abnormal_min { set; get; }
        /// <summary>
        /// 异常范围最大值
        /// </summary>
       public decimal? abnormal_max { set; get; }
        /// <summary>
        /// 严重范围最小值
        /// </summary>
       public decimal? serious_min { set; get; }
        /// <summary>
        /// 严重范围最大值
        /// </summary>
       public decimal? serious_max { set; get; }
       /// <summary>
       /// 触发通知逻辑
       /// </summary>
       public int? notice_logic_id { set; get; }
    }

    public class standard_detail : standard
    {
        [SugarColumn(IsIgnore = true)]
        public tag_info tag_info { set; get; }
        [SugarColumn(IsIgnore = true)]
        public machine machine { set; get; }
        [SugarColumn(IsIgnore = true)]
        public tag_type_sub tag_type_sub { set; get; }
        [SugarColumn(IsIgnore = true)]
        public area_node area_node { set; get; }
        [SugarColumn(IsIgnore = true)]
        public area_layer area_layer { set; get; }
        [SugarColumn(IsIgnore = true)]
        public notice_logic notice_logic { set; get; }
    }
}
