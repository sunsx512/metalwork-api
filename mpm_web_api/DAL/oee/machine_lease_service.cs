using mpm_web_api.db;
using mpm_web_api.model;
using mpm_web_api.model.m_oee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL.oee
{
    public class machine_lease_service: SqlSugarBase
    {
        public  List<machine_lease_detail> QueryableToList()
        {
            var list = DB.Queryable<machine_lease_detail>()
            .Mapper((it) =>
            {
                List<machine> machines = DB.Queryable<machine>().Where(x => x.id == it.machine_id).ToList();
                it.machine = machines.FirstOrDefault();
            }).OrderBy(x=>x.id).ToList();
            return list;
        }
    }
}
