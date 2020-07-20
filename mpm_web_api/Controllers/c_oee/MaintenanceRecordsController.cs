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
    [ApiExplorerSettings(GroupName = "OEE")]
    [Produces(("application/json"))]
    [Route("api/v1/configuration/oee/maintenance_records")]
    [SwaggerTag("保养记录")]
    [ApiController]
    public class MaintenanceRecordsController : Controller
    {
        ControllerHelper<maintenance_records> ch = new ControllerHelper<maintenance_records>();


        /// <summary>
        /// 获取
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<maintenance_records>> Get()
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
        public ActionResult<common.response> Post(maintenance_records t)
        {
            return Json(ch.Post(t));
        }
    }
}
