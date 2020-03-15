using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_common
{
    [SugarTable("common.api_request_log")]
    public class api_request_log
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        public int id { set; get; }
        public string path { set; get; }
        public string method { set; get; }
        public long cost_time { set; get; }
        public DateTime insert_time { set; get; }
    }
}
