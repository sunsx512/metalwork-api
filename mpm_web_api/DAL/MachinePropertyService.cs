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
    public class MachinePropertyService : SqlSugarBase
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public List<machine_property> QueryShift()
        {
            List<machine_property> list = DB.Queryable<machine_property>().Where(x => x.name_en == "shift").ToList();
            return list;
        }

        public bool AddShift(int machine_id, string day_start_time, string day_end_time, string night_start_time, string night_end_time)
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
            machine_property ap = new machine_property();
            ap.machine_id = machine_id;
            ap.name_cn = "班别";
            ap.name_en = "shift";
            ap.name_tw = "班别";
            ap.description = "班别";
            ap.format = JsonConvert.SerializeObject(shift);
            return DB.Insertable(ap).ExecuteCommand() > 0;
        }


        /// <summary>
        /// 固定排休时间
        /// </summary>
        /// <returns></returns>
        public List<machine_property> QueryFixedBreak()
        {
            List<machine_property> list = DB.Queryable<machine_property>().Where(x => x.name_en == "fixed_break").ToList();
            return list;
        }
        /// <summary>
        /// 获取时区信息
        /// </summary>
        /// <returns></returns>
        public List<machine_property> QueryTimeZone()
        {
            List<machine_property> list = DB.Queryable<machine_property>().Where(x => x.name_en == "time_zone").ToList();
            return list;
        }

        public bool AddFixedBreak(int machine_id, List<day> times)
        {
            machine_property ap = new machine_property();
            ap.machine_id = machine_id;
            ap.name_cn = "固定排休";
            ap.name_en = "fixed_break";
            ap.name_tw = "固定排休";
            ap.description = "固定排休";
            ap.format = JsonConvert.SerializeObject(times);
            return DB.Insertable(ap).ExecuteCommand() > 0;
        }



        /// <summary>
        /// 非固定排休时间
        /// </summary>
        /// <returns></returns>
        public List<machine_property> QueryUnfixedBreak()
        {
            List<machine_property> list = DB.Queryable<machine_property>().Where(x => x.name_en == "unfixed_break").ToList();
            return list;
        }
    }
}
