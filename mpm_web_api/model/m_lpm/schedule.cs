using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_lpm
{
    [SugarTable("lpm.schedule")]
    public class schedule:base_model
    {
        //计划名称
        public string name { set; get; }
        //开始时间
        public DateTime start_time { set; get; }
        //结束时间
        public DateTime end_time { set; get; }
    }
}
