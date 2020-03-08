using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("common.srp_inner_log")]
    public class srp_inner_log
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]

        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// srp模块名
        /// </summary>
        public string srp_code { get; set; }
        /// <summary>
        /// 插入时间
        /// </summary>
        public DateTime insert_time { get; set; }
        
    }
}
