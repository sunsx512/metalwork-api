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
        public List<machine> GetMachines(int pageNum, int pageSize, ref int count)
        {
            List<machine> rdc = DB.Queryable<machine>().ToList();
            if (rdc != null)
            {
                if (rdc.Count > 0)
                {
                    count = rdc.Count;
                    if (rdc.Count > (pageNum - 1) * pageSize)
                    {
                        if (rdc.Count > pageNum * pageSize)
                            rdc = rdc.GetRange((pageNum - 1) * pageSize, pageSize);
                        else
                            rdc = rdc.GetRange((pageNum - 1) * pageSize, rdc.Count - (pageNum - 1) * pageSize);
                        return rdc;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return null;
        }
        public List<machine> GetMachinesByName(string name,int pageNum, int pageSize,ref int count)
        {
            List<machine> rdc = DB.Queryable<machine>().Where(x=>x.name_cn.Contains(name)).ToList();
            if (rdc != null)
            {
                if (rdc.Count > 0)
                {
                    count = rdc.Count;
                    if (rdc.Count > (pageNum - 1) * pageSize)
                    {
                        if (rdc.Count > pageNum * pageSize)
                            rdc = rdc.GetRange((pageNum - 1) * pageSize, pageSize);
                        else
                            rdc = rdc.GetRange((pageNum - 1) * pageSize, rdc.Count - (pageNum - 1) * pageSize);
                        return rdc;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return null;
        }
    }
}
