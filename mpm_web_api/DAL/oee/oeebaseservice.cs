using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mpm_web_api.db;
using SqlSugar;
using mpm_web_api.model;

namespace mpm_web_api.DAL.oee
{
    public class oeebaseservice<T> : SqlSugarBase where T : class, new()
    {

    }
}
