using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.model;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_work_order
{
    [ApiExplorerSettings(GroupName = "WorkOrder")]
    [Produces(("application/json"))]
    [Route("api/v1/client/work_order/virtual_line_log")]
    [SwaggerTag("虚拟线日志")]
    [ApiController]
    public class VirtualLineLogController : Controller
    {
        ControllerHelper<virtual_line_log> ch = new ControllerHelper<virtual_line_log>();
        /// <summary>
        /// 添加虚拟线日志
        /// </summary>
        /// <param name="t"></param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPost]
        public ActionResult<common.response> Post(virtual_line_log t)
        {
            return Json(ch.Post(t));
        }

        /// <summary>
        /// 修改虚拟线日志
        /// </summary>
        /// <param name="t"></param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPut]
        public ActionResult<common.response> Put(virtual_line_log t)
        {
            return Json(ch.Put(t));
        }
    }
}