using mpm_web_api.db;
using mpm_web_api.model;
using mpm_web_api.model.m_common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL
{
    public class AreaPropertyService: SqlSugarBase
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public  List<area_property> QueryShift()
        {
            List<area_property> list = DB.Queryable<area_property>().Where(x => x.name_en == "shift").ToList();
            return list;
        }

        public bool AddShift(int area_node_id, string day_start_time, string day_end_time, string night_start_time, string night_end_time)
        {
            Shift shift = new Shift();
            day day = new day();
            night night = new night();
            day.start = day_start_time;
            day.end = day_end_time;
            night.start = night_start_time;
            night.end = night_end_time;
            shift.day = day;
            shift.night = night;
            area_property ap = new area_property();
            ap.area_node_id = area_node_id;
            ap.name_cn = "班别";
            ap.name_en = "shift";
            ap.name_tw = "班别";
            ap.description = "班别";
            ap.format = JsonConvert.SerializeObject(shift);
            return DB.Insertable<area_property>(ap).ExecuteCommand() > 0;
        }


        /// <summary>
        /// 固定排休时间
        /// </summary>
        /// <returns></returns>
        public  List<area_property> QueryFixedBreak()
        {
            List<area_property> list = DB.Queryable<area_property>().Where(x => x.name_en == "fixed_break").ToList();
            return list;
        }

        public bool AddFixedBreak(int area_node_id, List<day> times)
        {
            area_property ap = new area_property();
            ap.area_node_id = area_node_id;
            ap.name_cn = "固定排休";
            ap.name_en = "fixed_break";
            ap.name_tw = "固定排休";
            ap.description = "固定排休";
            ap.format = JsonConvert.SerializeObject(times);
            return DB.Insertable<area_property>(ap).ExecuteCommand() > 0;
        }



        /// <summary>
        /// 非固定排休时间
        /// </summary>
        /// <returns></returns>
        public  List<area_property> QueryUnfixedBreak()
        {
            List<area_property> list = DB.Queryable<area_property>().Where(x => x.name_en == "unfixed_break").ToList();
            return list;
        }

        
    }
}
