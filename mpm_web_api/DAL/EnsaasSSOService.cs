using mpm_web_api.db;
using MySql.Data.MySqlClient.Memcached;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wise_Pass;

namespace mpm_web_api.DAL
{
    public class EnsaasSSOService: SqlSugarBase
    {
        public void CreateClient()
        {
            //Client client = SSO.CreateClient("", "Metalwork");
        }
    }
}
