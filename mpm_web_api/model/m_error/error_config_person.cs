using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace mpm_web_api.model
{
    [SugarTable("andon.error_config_person")]
    public class error_config_person
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int error_config_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shift { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int person_level { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int person_id{ get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool isowner { get; set; }
    }
}
