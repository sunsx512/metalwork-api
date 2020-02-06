using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("andon.notification_group")]
    public class notification_group:base_model
    {
        public string name_en { get; set; }
        public string name_cn { get; set; }
        public string name_tw { get; set; }
        public string description { get; set; }

    }

    public class notification_group_detail : notification_group
    {
        //[SqlSugar.SugarColumn(IsIgnore = true)]
        //public  List<notification_person> persons { get;set;}
        [SqlSugar.SugarColumn(IsIgnore = true)]
        public List<notification_person_detail> person { get; set; }

    }
}
