using mpm_web_api.db;
using mpm_web_api.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL
{
    public class AreaNodeService:SqlSugarBase
    {
        public  List<area_node_detail> QueryableToList()
        {
            var list = DB.Queryable<area_node_detail>()
            .Mapper((it) =>
            {
                List<area_property> property = DB.Queryable<area_property>().Where(x => x.area_node_id == it.id).ToList();
                it.id = it.id;
                it.name_cn = it.name_cn;
                it.name_en = it.name_en;
                it.name_tw = it.name_tw;
                it.upper_id = it.upper_id;
                it.description = it.description;
                it.property = property;
            }).OrderBy(x=>x.id).ToList();
            return list;
        }
    }
}
