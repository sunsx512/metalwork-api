using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_common
{
    [SugarTable("common.wise_paas_user")]
    public class wise_paas_user
    {
        [SugarColumn(IsIdentity = true, ColumnName = "id")]
        public int id { set; get; }
        [SugarColumn(IsPrimaryKey = true,  ColumnName = "name")]
        //用户名
        public string name { set; get; }
        //密码
        public string password { set; get; }
        //角色
        public string role { set; get; }
    }
}
