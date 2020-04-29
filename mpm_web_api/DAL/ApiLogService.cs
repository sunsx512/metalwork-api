using mpm_web_api.Common;
using mpm_web_api.db;
using mpm_web_api.model.m_common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL
{
    public class ApiLogService: SqlSugarBase
    {
        public bool InsertExceptionLog(api_exception_log entity)
        {
            entity.insert_time.AddHours(GlobalVar.db_time_zone);
            return DB.Insertable<api_exception_log>(entity).ExecuteCommand() > 0;
        }

        public bool InsertRequestLog(api_request_log entity)
        {
            entity.insert_time.AddHours(GlobalVar.db_time_zone);
            return DB.Insertable<api_request_log>(entity).ExecuteCommand() > 0;
        }
    }
}
