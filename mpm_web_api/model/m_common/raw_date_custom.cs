using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_common
{
    [SugarTable("common.raw_date_custom")]
    public class raw_date_custom : base_model
    {
        public int tag_info_id { set; get; }
        public int machine_id { set; get; }
        public string value { set; get; }
        public DateTime insert_time { set; get; }
    }
}
