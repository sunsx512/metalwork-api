﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.model;

/// <summary>
/// add by sunsx.sun
/// 
/// Area_Node控制器
/// </summary>
namespace mpm_web_api.Controllers.c_common
{
    [Produces(("application/json"))]
    [Route("api/v1/configuration/public/area_node")]
    [ApiController]
    public class Area_NodeController : SSOController,IController<area_node>
    {
        ControllerHelper<area_node> ch = new ControllerHelper<area_node>();

        /// <summary>
        /// 获取所有Area_Node信息
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<area_node>> Get()
        {
            return Json(ch.Get());
        }
        /// <summary>
        /// 新增Area_Node
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPost]
        public ActionResult<common.response> Post(area_node t)
        {
            return Json(ch.Post(t));
        }
        /// <summary>
        /// 更新Area_Node
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPut]
        public ActionResult<common.response> Put(area_node t)
        {
            return Json(ch.Put(t));
        }
        /// <summary>
        /// 删除Area_Node
        /// </summary>
        /// <param name="id">主键id</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpDelete]
        public ActionResult<common.response> Delete(int id)
        {
            return Json(ch.Delete(id));
        }
    }
}