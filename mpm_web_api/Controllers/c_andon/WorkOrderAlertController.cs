using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.model;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_andon
{
    [ApiExplorerSettings(GroupName = "Andon")]
    [Produces(("application/json"))]
    [Route("/api/v1/configuration/andon/work_order_alert")]
    [SwaggerTag("工单预警信息")]
    [ApiController]
    public class WorkOrderAlertController : Controller
    {
        ControllerHelper<work_order_alert> ch = new ControllerHelper<work_order_alert>();
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">id</param>
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
        /// 查询
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<work_order_alert>> Get()
        {
            return Json(ch.Get());
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPost]
        public ActionResult<common.response> Post(work_order_alert t)
        {
            return Json(ch.Post(t));
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="t"></param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPut]
        public ActionResult<common.response> Put(work_order_alert t)
        {
            return Json(ch.Put(t));
        }
    }
}