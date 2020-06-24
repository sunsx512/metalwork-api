using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_lpm
{
    [SugarTable("lpm.person_shift")]
    public class person_shift:base_model
    {
        //人员id
        public int person_id { set; get; }
        //排班计划id
        public int schedule_id { set; get; }
        //班别
        public int shift { set; get; }
        //设备id
        public int machine_id { set; get; }
    }
}
