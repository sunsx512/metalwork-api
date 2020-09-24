                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using mpm_web_api.Common;
using mpm_web_api.DAL;
using mpm_web_api.DB;
using mpm_web_api.model;
using mpm_web_api.model.m_common;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_common
{
    [ApiExplorerSettings(GroupName = "Common")]
    [Produces(("application/json"))]
    [Route("api/v1/raw_data")]
    [SwaggerTag("获取历史数据")]
    [ApiController]
    public class RawDataController : Microsoft.AspNetCore.Mvc.Controller
    {
        TagService tagService = new TagService();
        MongoHelper mh = new MongoHelper();
        /// <summary>
        /// 按标签获取历史数据
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet("tag")]
        public ActionResult<common.response<raw_data>> Get(string tag,DateTime start_time, DateTime end_time)
        {
            start_time = start_time.AddHours(-GlobalVar.time_zone);
            end_time = end_time.AddHours(-GlobalVar.time_zone);
            tag_info_detail tag_Info_Detail = tagService.QueryableByTag(tag);
            List<raw_data> res = new List<raw_data>();
            string scada_id = tag.Split(':')[0];
            string tag_name = tag.Split(':')[1];
            object obj;
            var filterBuilder1 = Builders<MongoDbTag>.Filter;
            var filter1 = filterBuilder1.And(filterBuilder1.Gt(x => x.ts, start_time),
                                                filterBuilder1.Lte(x => x.ts, end_time),
                                                filterBuilder1.Eq(x => x.s, scada_id),
                                                filterBuilder1.Eq(x => x.t, tag_name));
            List<MongoDbTag> list = mh.GetList("scada_HistRawData", filter1);
            if (list != null)
            {
                if(list.Count > 0)
                {
                    if(tag_Info_Detail!= null)
                    {
                        foreach (MongoDbTag mongoDbTag in list)
                        {
                            raw_data raw_Data = new raw_data();
                            raw_Data.machine_name = tag_Info_Detail.machine.name_en;
                            raw_Data.tag_type_sub = tag_Info_Detail.tag_type_sub.name_en;
                            raw_Data.name = tag;
                            raw_Data.value = mongoDbTag.v.ToString();
                            raw_Data.ts = mongoDbTag.ts;
                            res.Add(raw_Data);
                        }
                    }
                    else
                    {
                        foreach (MongoDbTag mongoDbTag in list)
                        {
                            raw_data raw_Data = new raw_data();
                            raw_Data.name = tag;
                            raw_Data.value = mongoDbTag.v.ToString();
                            raw_Data.ts = mongoDbTag.ts;
                            res.Add(raw_Data);
                        }
                    }
                }
            }
            obj = common.ResponseStr((int)httpStatus.succes, "调用成功", res);
            return Json(obj);
        }

        [HttpGet("tag/separate")]
        public ActionResult<common.response<raw_data>> GetD(string tag, DateTime start_time, DateTime end_time, int start_index, int count)
        {
            start_time = start_time.AddHours(-GlobalVar.time_zone);
            end_time = end_time.AddHours(-GlobalVar.time_zone);
            tag_info_detail tag_Info_Detail = tagService.QueryableByTag(tag);
            List<raw_data> res = new List<raw_data>();
            string scada_id = tag.Split(':')[0];
            string tag_name = tag.Split(':')[1];
            object obj;
            var filterBuilder1 = Builders<MongoDbTag>.Filter;
            var filter1 = filterBuilder1.And(filterBuilder1.Gt(x => x.ts, start_time),
                                                filterBuilder1.Lte(x => x.ts, end_time),
                                                filterBuilder1.Eq(x => x.s, scada_id),
                                                filterBuilder1.Eq(x => x.t, tag_name));
            List<MongoDbTag> list = mh.GetList("scada_HistRawData", filter1);
            if (list != null)
            {
                if (list.Count > 0)
                {
                    list = list.OrderBy(x => x.ts).ToList().GetRange(start_index, count);
                    if (tag_Info_Detail != null)
                    {
                        foreach (MongoDbTag mongoDbTag in list)
                        {
                            raw_data raw_Data = new raw_data();
                            raw_Data.machine_name = tag_Info_Detail.machine.name_en;
                            raw_Data.tag_type_sub = tag_Info_Detail.tag_type_sub.name_en;
                            raw_Data.name = tag;
                            raw_Data.value = mongoDbTag.v.ToString();
                            raw_Data.ts = mongoDbTag.ts;
                            res.Add(raw_Data);
                        }
                    }
                    else
                    {
                        foreach (MongoDbTag mongoDbTag in list)
                        {
                            raw_data raw_Data = new raw_data();
                            raw_Data.name = tag;
                            raw_Data.value = mongoDbTag.v.ToString();
                            raw_Data.ts = mongoDbTag.ts;
                            res.Add(raw_Data);
                        }
                    }
                }
            }
            obj = common.ResponseStr((int)httpStatus.succes, "调用成功", res);
            return Json(obj);
        }



        /// <summary>
        /// 按设备名获取历史数据
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet("machine")]
        public ActionResult<common.response<raw_data>> GetM(string machine, DateTime start_time, DateTime end_time)
        {
            List<tag_info_detail> tag_Info_Details = tagService.QueryableByMachine(machine);
            List<raw_data> res = new List<raw_data>();
            start_time = start_time.AddHours(-GlobalVar.time_zone);
            end_time = end_time.AddHours(-GlobalVar.time_zone);
            if (tag_Info_Details != null)
            {
                foreach(var tag in tag_Info_Details)
                {
                    string scada_id = tag.name.Split(':')[0];
                    string tag_name = tag.name.Split(':')[1];
                    var filterBuilder1 = Builders<MongoDbTag>.Filter;
                    var filter1 = filterBuilder1.And(filterBuilder1.Gt(x => x.ts, start_time),
                                                        filterBuilder1.Lte(x => x.ts, end_time),
                                                        filterBuilder1.Eq(x => x.s, scada_id),
                                                        filterBuilder1.Eq(x => x.t, tag_name));
                    List<MongoDbTag> list = mh.GetList("scada_HistRawData", filter1);
                    if (list != null)
                    {
                        if (list.Count > 0)
                        {
                            List<raw_data> rds = new List<raw_data>();
                            foreach (MongoDbTag mongoDbTag in list)
                            {
                                raw_data raw_Data = new raw_data();
                                raw_Data.machine_name = tag.machine.name_en;
                                raw_Data.tag_type_sub = tag.tag_type_sub.name_en;
                                raw_Data.name = tag.name;
                                raw_Data.value = mongoDbTag.v.ToString();
                                raw_Data.ts = mongoDbTag.ts;
                                res.Add(raw_Data);
                            }
                        }
                    }
                }
            }
            object obj = common.ResponseStr((int)httpStatus.succes, "调用成功", res);
            return Json(obj);
        }


        [HttpGet("machine/separate")]
        public ActionResult<common.response<raw_data>> GetMD(string machine, DateTime start_time, DateTime end_time, int start_index, int count)
        {
            List<tag_info_detail> tag_Info_Details = tagService.QueryableByMachine(machine);
            List<raw_data> res = new List<raw_data>();
            start_time = start_time.AddHours(-GlobalVar.time_zone);
            end_time = end_time.AddHours(-GlobalVar.time_zone);
            if (tag_Info_Details != null)
            {
                foreach (var tag in tag_Info_Details)
                {
                    string scada_id = tag.name.Split(':')[0];
                    string tag_name = tag.name.Split(':')[1];
                    var filterBuilder1 = Builders<MongoDbTag>.Filter;
                    var filter1 = filterBuilder1.And(filterBuilder1.Gt(x => x.ts, start_time),
                                                        filterBuilder1.Lte(x => x.ts, end_time),
                                                        filterBuilder1.Eq(x => x.s, scada_id),
                                                        filterBuilder1.Eq(x => x.t, tag_name));
                    List<MongoDbTag> list = mh.GetList("scada_HistRawData", filter1);
                    
                    if (list != null)
                    {
                        if (list.Count > 0)
                        {
                            list = list.OrderBy(x => x.ts).ToList().GetRange(start_index, count);
                            List<raw_data> rds = new List<raw_data>();
                            foreach (MongoDbTag mongoDbTag in list)
                            {
                                raw_data raw_Data = new raw_data();
                                raw_Data.machine_name = tag.machine.name_en;
                                raw_Data.tag_type_sub = tag.tag_type_sub.name_en;
                                raw_Data.name = tag.name;
                                raw_Data.value = mongoDbTag.v.ToString();
                                raw_Data.ts = mongoDbTag.ts;
                                res.Add(raw_Data);
                            }
                        }
                    }
                }
            }
            object obj = common.ResponseStr((int)httpStatus.succes, "调用成功", res);
            return Json(obj);
        }
    }
}
