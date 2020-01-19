using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.model;
using Swashbuckle.AspNetCore.Annotations;

/// <summary>
/// add by sunsx.sun
/// 
/// Wechart_Server控制器
/// </summary>
namespace mpm_web_api.Controllers.c_common
{
    [Produces(("application/json"))]
    [Route("api/v1/configuration/public/wechart_server")]
    [SwaggerTag("操作微信服务数据")]
    [ApiController]
    public class Wechart_ServerController : SSOController,IController<wechart_server>
    {
        ControllerHelper<wechart_server> ch = new ControllerHelper<wechart_server>();

        /// <summary>
        /// 获取所有Wechart_Server信息
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<wechart_server>> Get()
        {
            return Json(ch.Get());
        }
        /// <summary>
        /// 新增Wechart_Server
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPost]
        public ActionResult<common.response> Post(wechart_server t)
        {
            return Json(ch.Post(t));
        }
        /// <summary>
        /// 更新Wechart_Server
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPut]
        public ActionResult<common.response> Put(wechart_server t)
        {
            return Json(ch.Put(t));
        }
        /// <summary>
        /// 删除Wechart_Server
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