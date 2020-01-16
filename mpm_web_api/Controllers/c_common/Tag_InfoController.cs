using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.DAL;
using mpm_web_api.model;

namespace mpm_web_api.Controllers
{
    [Produces(("application/json"))]
    [Route("api/v1/configuration/public/tag_info")]
    [ApiController]
    public class Tag_InfoController : SSOController,IController<tag_info>
    {
        ControllerHelper<tag_info> ch = new ControllerHelper<tag_info>();
        /// <summary>
        /// 删除设备
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
        /// <summary>
        /// 获取所有设备信息
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<tag_info>> Get()
        {
            return Json(ch.Get());
        }
        /// <summary>
        /// 新增设备
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPost]
        public ActionResult<common.response> Post(tag_info t)
        {
            return Json(ch.Post(t));
        }
        /// <summary>
        /// 更新设备
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPut]
        public ActionResult<common.response> Put(tag_info t)
        {
            return Json(ch.Put(t));
        }
    }
}