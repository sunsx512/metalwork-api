using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.model.m_oee;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_oee
{
    [Produces(("application/json"))]
    [Route("api/v1/configuration/oee/machine_lease")]
    [SwaggerTag("租赁信息配置")]
    [ApiController]
    public class MachineLeaseController : SSOController
    {
        ControllerHelper<machine_lease> ch = new ControllerHelper<machine_lease>();
        /// <summary>
        /// 删除
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
        /// 获取
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<machine_lease>> Get()
        {
            return Json(ch.Get());
        }

        /// <summary>
        /// 新增标签
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPost]
        public ActionResult<common.response> Post(machine_lease t)
        {
            return Json(ch.Post(t));
        }
        /// <summary>
        /// 更新标签信息
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPut]
        public ActionResult<common.response> Put(machine_lease t)
        {
            return Json(ch.Put(t));
        }
    }
}