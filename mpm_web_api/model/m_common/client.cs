using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_common
{
    [SugarTable("common.wise_paas_client")]
    public class client:base_model
    {
        public string client_id { set; get; }
        public string client_secret { set; get; }
    }
}
