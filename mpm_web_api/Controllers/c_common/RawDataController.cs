                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using mpm_web_api.Common;
using mpm_web_api.DAL;
using mpm_web_api.DAL.oee;
using mpm_web_api.DB;
using mpm_web_api.model;
using mpm_web_api.model.m_common;
using mpm_web_api.model.m_oee;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_common
{
    [ApiExplorerSettings(GroupName = "Common")]
    [Produces(("application/json"))]
    [Route("api/v1/raw_data")]
    [SwaggerTag("获取历史数据")]
    [ApiController]
    public class RawDataController : Controller
    {
        TagService tagService = new TagService();
        RawDateCustomService rawDateCustomService = new RawDateCustomService();
        tag_value_service tag_Value_Service = new tag_value_service();
        /// <summary>
        /// 按标签获取历史数据
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet("tag")]
        public ActionResult<common.response<raw_data>> Get(string tag, DateTime start_time, DateTime end_time)
        {
            tag_info_detail tag_Info_Detail = tagService.QueryableByTag(tag);
            List<raw_data> res = new List<raw_data>();
            List<raw_date_custom> rdc = rawDateCustomService.Query(tag, start_time, end_time);
            foreach(raw_date_custom et in rdc)
            {
                raw_data rd = new raw_data();
                rd.machine_name = tag_Info_Detail.machine.name_en;
                rd.name = tag;
                rd.tag_type_sub = tag_Info_Detail.tag_type_sub.name_en;
                rd.ts = et.insert_time;
                rd.value = et.value.ToString();
                res.Add(rd);
            }     
            object obj = common.ResponseStr((int)httpStatus.succes, "调用成功", res);
            return Json(obj);
        }

        [HttpGet("tag/separate")]
        public ActionResult<common.responsewithcount<raw_data>> GetD(string tag, DateTime start_time, DateTime end_time, int pageNum, int pageSize)
        {
            tag_info_detail tag_Info_Detail = tagService.QueryableByTag(tag);
            List<raw_data> res = new List<raw_data>();
            object obj;
            int count = 0;
            List<raw_date_custom> rdc = rawDateCustomService.Query(tag, start_time, end_time);
            if (rdc != null)
            {
                if (rdc.Count > 0)
                {
                    count = rdc.Count;
                    if (rdc.Count > (pageNum - 1) * pageSize)
                    {
                        if (rdc.Count > pageNum * pageSize)
                            rdc = rdc.GetRange((pageNum - 1) * pageSize, pageSize);
                        else
                            rdc = rdc.GetRange((pageNum - 1) * pageSize, rdc.Count - (pageNum - 1) * pageSize);
                        foreach (raw_date_custom et in rdc)
                        {
                            raw_data rd = new raw_data();
                            rd.machine_name = tag_Info_Detail.machine.name_en;
                            rd.name = tag;
                            rd.tag_type_sub = tag_Info_Detail.tag_type_sub.name_en;
                            rd.ts = et.insert_time;
                            rd.value = et.value.ToString();
                            res.Add(rd);
                        }
                    }
                    else
                    {
                        res = null;
                        object obji = common.ResponseStr((int)httpStatus.succes, "调用成功", res);
                        return Json(obji);
                    }
                }
            }
            obj = common.ResponseStr((int)httpStatus.succes, "调用成功",count, res);
            return Json(obj);
        }
        /// <summary>
        /// 按照时间排序
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="start_time"></param>
        /// <param name="end_time"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("tag/separate/orderByTime")]
        public ActionResult<common.responsewithcount<raw_data>> GetDT(string tag, DateTime start_time, DateTime end_time, int pageNum, int pageSize)
        {
            tag_info_detail tag_Info_Detail = tagService.QueryableByTag(tag);
            List<raw_data> res = new List<raw_data>();
            object obj;
            int count = 0;
            List<raw_date_custom> rdc = rawDateCustomService.Query(tag, start_time, end_time).OrderByDescending(x=>x.insert_time).ToList();
            if (rdc != null)
            {
                if (rdc.Count > 0)
                {
                    count = rdc.Count;
                    if (rdc.Count > (pageNum - 1) * pageSize)
                    {
                        if (rdc.Count > pageNum * pageSize)
                            rdc = rdc.GetRange((pageNum - 1) * pageSize, pageSize);
                        else
                            rdc = rdc.GetRange((pageNum - 1) * pageSize, rdc.Count - (pageNum - 1) * pageSize);
                        foreach (raw_date_custom et in rdc)
                        {
                            raw_data rd = new raw_data();
                            rd.machine_name = tag_Info_Detail.machine.name_en;
                            rd.name = tag;
                            rd.tag_type_sub = tag_Info_Detail.tag_type_sub.name_en;
                            rd.ts = et.insert_time;
                            rd.value = et.value.ToString();
                            res.Add(rd);
                        }
                    }
                    else
                    {
                        res = null;
                        object obji = common.ResponseStr((int)httpStatus.succes, "调用成功", res);
                        return Json(obji);
                    }
                }
            }
            obj = common.ResponseStr((int)httpStatus.succes, "调用成功", count, res);
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
            List<raw_date_custom> rdc = rawDateCustomService.QueryByMachine(machine, start_time, end_time);
            foreach (raw_date_custom et in rdc)
            {
                tag_info_detail _tag_Info_Detail = tag_Info_Details.Where(x => x.id == et.tag_info_id).FirstOrDefault();
                raw_data rd = new raw_data();
                rd.machine_name = machine;
                rd.name = _tag_Info_Detail.name;
                rd.tag_type_sub = _tag_Info_Detail.tag_type_sub.name_en;
                rd.ts = et.insert_time;
                rd.value = et.value.ToString();
                res.Add(rd);
            }
            object obj = common.ResponseStr((int)httpStatus.succes, "调用成功", res);
            return Json(obj);
        }


        [HttpGet("machine/separate")]
        public ActionResult<common.responsewithcount<raw_data>> GetMD(string machine, DateTime start_time, DateTime end_time, int pageNum, int pageSize)
        {
            List<tag_info_detail> tag_Info_Details = tagService.QueryableByMachine(machine);
            List<raw_data> res = new List<raw_data>();
            List<raw_date_custom> rdc = rawDateCustomService.QueryByMachine(machine, start_time, end_time);
            int count = 0;
            if (rdc != null)
            {
                if (rdc.Count > 0)
                {
                    count = rdc.Count;
                    if (rdc.Count > (pageNum - 1) * pageSize)
                    {
                        if (rdc.Count > pageNum * pageSize)
                            rdc = rdc.GetRange((pageNum - 1) * pageSize, pageSize);
                        else
                            rdc = rdc.GetRange((pageNum - 1) * pageSize, rdc.Count - (pageNum - 1) * pageSize);
                        foreach (raw_date_custom et in rdc)
                        {
                            tag_info_detail _tag_Info_Detail = tag_Info_Details.Where(x => x.id == et.tag_info_id).FirstOrDefault();
                            raw_data rd = new raw_data();
                            rd.machine_name = machine;
                            rd.name = _tag_Info_Detail.name;
                            rd.tag_type_sub = _tag_Info_Detail.tag_type_sub.name_en;
                            rd.ts = et.insert_time;
                            rd.value = et.value.ToString();
                            res.Add(rd);
                        }
                    }
                    else
                    {
                        res = null;
                        object obji = common.ResponseStr((int)httpStatus.succes, "调用成功", res);
                        return Json(obji);
                    }
                }
            }
            object obj = common.ResponseStr((int)httpStatus.succes, "调用成功", count, res);
            return Json(obj);
        }


        [HttpGet("realtime")]
        public ActionResult<common.responsewithcount<tag_info_value>> GetRT(int tag_id = 0)
        {
            List<tag_info_value> list = new List<tag_info_value>();
            if(tag_id == 0)
            {
                list = tag_Value_Service.GetList();
            }
            else
            {
                list = tag_Value_Service.GetList().Where(x=>x.tag_info_id == tag_id).ToList();
            }
            object obj = common.ResponseStr((int)httpStatus.succes, "调用成功", list);
            return Json(obj);
        }
    }
}
