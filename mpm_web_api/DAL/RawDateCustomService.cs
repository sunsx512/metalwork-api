using mpm_web_api.db;
using mpm_web_api.model;
using mpm_web_api.model.m_common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL
{
    public class RawDateCustomService : SqlSugarBase
    {
        public List<raw_date_custom> Query(string tag_name,DateTime start_time,DateTime end_time)
        {
            tag_info tag = DB.Queryable<tag_info>().Where(x => x.name == tag_name)?.First();
            List<raw_date_custom> raw_date_customs = null;
            string start_date = start_time.ToShortDateString();
            string end_date = end_time.ToShortDateString();
            string table_name = "common.raw_date_custom";
            if (start_date == end_date)
            {
                string[] ss = start_date.Split('/');
                string month = (ss[1].Length <= 1) ? "0" + ss[1] : ss[1];
                string day = (ss[2].Length <= 1) ? "0" + ss[2] : ss[2];
                table_name = table_name + "_" + ss[0] + "_" + month + "_" + day;
            }
            if (tag != null)
            {
                raw_date_customs = DB.Queryable<raw_date_custom>()
                                    .AS(table_name)
                                    .Where(x => x.tag_info_id == tag.id )
                                    .Where(x => x.insert_time >= start_time && x.insert_time <= end_time)
                                    .ToList();                                                             
            }
            return raw_date_customs;
        }

        public List<raw_date_custom> QueryByMachine(string machine_name, DateTime start_time, DateTime end_time)
        {
            machine mc = DB.Queryable<machine>().Where(x => x.name_en == machine_name)?.First();
            List<raw_date_custom> raw_date_customs = null;
            string start_date = start_time.ToShortDateString();
            string end_date = end_time.ToShortDateString();
            string table_name = "common.raw_date_custom";
            if (start_date == end_date)
            {
                string[] ss = start_date.Split('/');
                string month = (ss[1].Length <= 1) ? "0" + ss[1] : ss[1];
                string day = (ss[2].Length <= 1) ? "0" + ss[2] : ss[2];
                table_name = table_name + "_" + ss[0] + "_" + month + "_" + day;
            }
            if (mc != null)
            {
                raw_date_customs = DB.Queryable<raw_date_custom>()
                                    .AS(table_name)
                                    .Where(x => x.machine_id == mc.id)
                                    .Where(x => x.insert_time >= start_time && x.insert_time <= end_time)
                                    .ToList();
            }
            return raw_date_customs;
        }
    }
}
