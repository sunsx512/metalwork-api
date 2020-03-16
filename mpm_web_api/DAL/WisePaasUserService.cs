using mpm_web_api.db;
using mpm_web_api.model.m_common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL
{
    public class WisePaasUserService: SqlSugarBase
    {
        public bool SaveUserInfo(wise_paas_user entity)
        {
            return DB.Saveable<wise_paas_user>(entity).ExecuteCommand() > 0;
        }
    }
}
