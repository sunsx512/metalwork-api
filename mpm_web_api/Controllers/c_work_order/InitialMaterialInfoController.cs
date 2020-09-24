using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.model.m_wo;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_work_order
{
    [ApiExplorerSettings(GroupName = "WorkOrder")]
    [Produces(("application/json"))]
    [Route("api/v1/configuration/work_order/initial_material_info")]
    [SwaggerTag("预设物料呼叫信息")]
    [ApiController]
    public class InitialMaterialInfoController : Microsoft.AspNetCore.Mvc.Controller
    {
        ControllerHelper<initial_material_info> ch = new ControllerHelper<initial_material_info>();
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键id</param>
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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<common.response<initial_material_info>> Get()
        {
            return Json(ch.Get());
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPost]
        public ActionResult<common.response> Post(initial_material_info t)
        {
            return Json(ch.Post(t));
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPut]
        public ActionResult<common.response> Put(initial_material_info t)
        {
            return Json(ch.Put(t));
        }
    }
}
