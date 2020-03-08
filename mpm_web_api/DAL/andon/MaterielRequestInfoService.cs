using mpm_web_api.db;
using mpm_web_api.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL.andon
{
    public class MaterielRequestInfoService : SqlSugarBase
    {
        public List<material_request_info> QueryableToListByMachineAndStatus(int status, string machine)
        {
            DateTime dt = new DateTime();
            List<material_request_info> list = new List<material_request_info>();
            switch (status)
            {
                case 0: list = DB.Queryable<material_request_info>()
                                                           .Where(x=>x.machine_name  == machine).ToList(); break;
                case 1: list = DB.Queryable<material_request_info>()
                                                           .Where(x => x.machine_name == machine)
                                                           .Where(x => x.take_time == dt).ToList(); break;
            }
            return list;
        }
    }
}
