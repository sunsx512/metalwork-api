using mpm_web_api.model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL
{
    public class NotificationGroupService : BaseService<notification_group>
    {
        public new List<notification_group_detail> QueryableToList()
        {
            var list = DB.Queryable<notification_group_detail>()
            .Mapper((it) =>
            {               
                List<notification_person_detail> notification_persons = DB.Queryable<notification_person_detail>().Where(x=>x.notification_group_id == it.id).ToList();               
                it.id = it.id;
                it.name_cn = it.name_cn;
                it.name_en = it.name_en;
                it.name_tw = it.name_tw;
                it.person = notification_persons;
            }).ToList();
            return list;
        }

        /// <summary>
        /// 删除该群组下的指定人员
        /// </summary>
        /// <param name="group_id"></param>
        /// <param name="person_id"></param>
        /// <returns></returns>
        public  bool DeletePerson(int group_id,int person_id)
        {
            return DB.Deleteable<notification_person>()
                    .Where(x => x.notification_group_id == group_id)
                    .Where(x => x.person_id == person_id)
                    .ExecuteCommand() > 0;
                                                          
        }


    }
}
