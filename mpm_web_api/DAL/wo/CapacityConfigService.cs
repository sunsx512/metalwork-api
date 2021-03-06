using mpm_web_api.Common;
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
            return DB.Queryable<capacity_config>().OrderBy(x=>x.id).ToList();
        }
        public bool Put(capacity_config entity)
        {
            DateTime dt = entity.date.AddHours(GlobalVar.time_zone);
            entity.date = dt;
            return DB.Updateable(entity).Where(x => x.id == entity.id).ExecuteCommand() > 0;
        }

        public bool Post(capacity_config entity)
        {
            DateTime dt = entity.date.AddHours(GlobalVar.time_zone);
            entity.date = dt;
            return DB.Insertable(entity).ExecuteCommand() > 0;
        }

        public bool Delete(int id)
        {
            return DB.Deleteable<capacity_config>().Where(x=>x.id == id).ExecuteCommand() > 0;
        }
    }
}
