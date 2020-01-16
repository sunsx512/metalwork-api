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
    [Route("api/v1/configuration/public/machine")]
    [ApiController]
    public class MachineController : SSOController,IController<machine>
    {
        ControllerHelper<machine> ch = new ControllerHelper<machine>();
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
        public ActionResult<common.response<machine>> Get()
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
        public ActionResult<common.response> Post(machine t)
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
        public ActionResult<common.response> Put(machine t)
        {
            return Json(ch.Put(t));
        }
    }
}