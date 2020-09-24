using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_common
{
    [SugarTable("common.wise_paas_client")]
    public class client
    {
        public string clientId { set; get; }
        public string clientSecret { set; get; }
        public int creationTime { set; get; }
        public int lastModifiedTime { set; get; }
        [SugarColumn(IsPrimaryKey = true, ColumnName = "appName")]
        public string appName { set; get; }
        public string appId { set; get; }
        public string serviceName { set; get; }
        public string cluster { set; get; }
        public string workspace { set; get; }
        public string @namespace { set; get; }
        public string datacenter { set; get; }
        [SugarColumn(IsIgnore = true)]
        public string redirectUrl { set; get; }
        [SugarColumn(IsIgnore = true)]
        public List<string> scopes { set; get; }
    }
}
