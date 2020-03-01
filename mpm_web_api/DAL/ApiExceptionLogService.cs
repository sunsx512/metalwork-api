﻿using mpm_web_api.db;
using mpm_web_api.model.m_common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL
{
    public class ApiExceptionLogService: SqlSugarBase
    {
        public bool InsertLog(api_exception_log entity)
        {
            return DB.Insertable<api_exception_log>(entity).ExecuteCommand() > 0;
        }
    }
}
