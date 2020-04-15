using mpm_web_api.db;
using mpm_web_api.model.m_wo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL.wo
{
    public class CapacityConfigService: SqlSugarBase
    {
        public List<capacity_config> Get()
        {
            return DB.Queryable<capacity_config>().ToList();
        }
        public bool Put(capacity_config entity)
        {
            return DB.Updateable(entity).ExecuteCommand() > 0;
        }

        public bool Post(capacity_config entity)
        {
            return DB.Insertable(entity).ExecuteCommand() > 0;
        }

        public bool Delete(int id)
        {
            return DB.Deleteable<capacity_config>(id).ExecuteCommand() > 0;
        }
    }
}
