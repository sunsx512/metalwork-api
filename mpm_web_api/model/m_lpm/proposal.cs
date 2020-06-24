using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_lpm
{
    [SugarTable("lpm.proposal")]
    public class proposal:base_model
    {
        //提案人员id
        public int person_id { set; get; }
        //标题
        public string title { set; get; }
        //内容
        public string content { set; get; }
    }
}
