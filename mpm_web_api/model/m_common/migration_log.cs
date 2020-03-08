using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_common
{
    [SugarTable("common.migration_log")]
    public class migration_log
    {
        public int id { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string migration_id { get; set; }
    }
}
