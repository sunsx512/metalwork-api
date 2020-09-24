using mpm_web_api.model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL
{
    public class TagService:BaseService<tag_info>
    {
        public new List<tag_info_detail> QueryableToList()
        {
            var list = DB.Queryable<tag_info_detail>()
            .Mapper((it) =>
            {
                List<tag_type_sub> tag_type_subs = DB.Queryable<tag_type_sub>().Where(x => x.id == it.tag_type_sub_id).ToList();
                List<tag_type> tag_types = DB.Queryable<tag_type>().Where(x => x.id == tag_type_subs.First().tag_type_id).ToList();
                List<machine> machines = DB.Queryable<machine>().Where(x => x.id == it.machine_id).ToList();
                it.id = it.id;
                it.machine = machines.FirstOrDefault();
                it.name = it.name;
                it.description = it.description;
                it.machine_id = it.machine_id;
                it.tag_type = tag_types.FirstOrDefault();
                it.tag_type_sub = tag_type_subs.FirstOrDefault();
            }).OrderBy(x=>x.id).ToList();
            return list;
        }

        public  tag_info_detail QueryableByTag(string tag)
        {
            tag_info_detail res = null;
            tag_info tag_Info = DB.Queryable<tag_info>().Where(x => x.name == tag)?.First();
            if(tag_Info != null)
            {
                res = new tag_info_detail();
                tag_type_sub tag_type_sub = DB.Queryable<tag_type_sub>().Where(x => x.id == tag_Info.tag_type_sub_id).First();
                tag_type tag_Type = DB.Queryable<tag_type>().Where(x => x.id == tag_type_sub.tag_type_id).First();
                machine machine = DB.Queryable<machine>().Where(x => x.id == tag_Info.machine_id).First();
                res.id = tag_Info.id;
                res.machine = machine;
                res.name = tag_Info.name;
                res.description = tag_Info.description;
                res.machine_id = tag_Info.machine_id;
                res.tag_type = tag_Type;
                res.tag_type_sub = tag_type_sub;
            }
            return res;
        }


        public List<tag_info_detail> QueryableByMachine(string machine)
        {
            List<tag_info_detail> list = null;
            machine machine1 = DB.Queryable<machine>().Where(x => x.name_en == machine)?.First();
            if(machine1 != null)
            {
                List<tag_info> tag_Infos = DB.Queryable<tag_info>().Where(x => x.machine_id == machine1.id).ToList();
                list = new List<tag_info_detail>();
                foreach (var tag_Info in tag_Infos)
                {
                    if (tag_Info != null)
                    {
                        
                        tag_info_detail res = new tag_info_detail();
                        tag_type_sub tag_type_sub = DB.Queryable<tag_type_sub>().Where(x => x.id == tag_Info.tag_type_sub_id).First();
                        tag_type tag_Type = DB.Queryable<tag_type>().Where(x => x.id == tag_type_sub.tag_type_id).First();
                        res.id = tag_Info.id;
                        res.machine = machine1;
                        res.name = tag_Info.name;
                        res.description = tag_Info.description;
                        res.machine_id = tag_Info.machine_id;
                        res.tag_type = tag_Type;
                        res.tag_type_sub = tag_type_sub;
                        list.Add(res);
                    }
                }
            }
            return list;
        }
    }
}
