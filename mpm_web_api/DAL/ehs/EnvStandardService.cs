using MongoDB.Driver;
using mpm_web_api.db;
using mpm_web_api.model;
using mpm_web_api.model.m_ehs;
using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL.ehs
{
    public class EnvStandardService : SqlSugarBase
    {
        public List<standard_detail> QueryableToList()
        {
            List<standard_detail> list = new List<standard_detail>();
            tag_type tagType = DB.Queryable<tag_type>().Where(x => x.name_en == "Environment").First();
            List<tag_type_sub> tagTypeSubs = DB.Queryable<tag_type_sub>().Where(x => x.tag_type_id == tagType.id).ToList();
            List<tag_info> tags = DB.Queryable<tag_info>().ToList();
            var tt = (from a in tags
                      join b in tagTypeSubs
                      on a.tag_type_sub_id equals b.id
                      //into ab
                      //from c in ab.DefaultIfEmpty()
                      select a);
            //人类迷惑行为  匿名对象→字符串→实体对象
            string str = JsonConvert.SerializeObject(tt); ;
            tags = JsonConvert.DeserializeObject<List<tag_info>>(str);
            if (tags != null)
            {
                foreach(tag_info tag in tags)
                {
                    standard_detail standard_Detail = new standard_detail();
                    standard standard = DB.Queryable<standard>().Where(x => x.tag_id == tag.id)?.First();
                    machine mc = DB.Queryable<machine>().Where(x => x.id == tag.machine_id)?.First();
                    tag_type_sub tts = DB.Queryable<tag_type_sub>().Where(x => x.id == tag.tag_type_sub_id)?.First();
                    
                    area_node an = new area_node();
                    area_layer al = new area_layer();
                    if (mc != null)
                    {
                        an = DB.Queryable<area_node>().Where(x => x.id == mc.area_node_id)?.First();
                        al = DB.Queryable<area_layer>().Where(x => x.id == an.area_layer_id)?.First();
                    }
                    standard_Detail.area_layer = al;
                    standard_Detail.area_node = an;
                    standard_Detail.machine = mc;
                    standard_Detail.tag_id = tag.id;
                    standard_Detail.tag_info = tag;
                    standard_Detail.tag_type_sub = tts;
                    standard_Detail.tag_type_sub_id = tts.id;
                    if(standard != null)
                    {
                        standard_Detail.id = standard.id;
                        standard_Detail.normal_min = standard.normal_min;
                        standard_Detail.normal_max = standard.normal_max;
                        standard_Detail.abnormal_max = standard.abnormal_max;
                        standard_Detail.abnormal_min = standard.abnormal_min;
                        standard_Detail.serious_max = standard.serious_max;
                        standard_Detail.serious_min = standard.serious_min;
                        if(standard.notice_logic_id != null)
                        {
                            notice_logic nt = DB.Queryable<notice_logic>().Where(x => x.id == standard.notice_logic_id)?.First();
                            standard_Detail.notice_logic = nt;
                        }

                    }
                    list.Add(standard_Detail);
                }
            }
            return list;
        }
    }
}
