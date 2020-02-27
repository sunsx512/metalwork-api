using mpm_web_api.db;
using mpm_web_api.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL
{
    public class MachineService: SqlSugarBase
    {
        public int GetMachineCount()
        {
            List<machine> list = DB.Queryable<machine>().ToList();
            if (list == null)
                return 0;
            else
                return list.Count;
        }
    }
}
