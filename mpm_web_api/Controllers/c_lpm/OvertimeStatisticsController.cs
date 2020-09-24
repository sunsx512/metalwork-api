using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.model.m_lpm;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_lpm
{
    [ApiExplorerSettings(GroupName = "LPM")]
    [Produces(("application/json"))]
    [Route("api/v1/configuration/lpm/overtime_statistics")]
    [SwaggerTag("加班统计")]
    [ApiController]
    public class OvertimeStatisticsController : Microsoft.AspNetCore.Mvc.Controller
    {
        ControllerHelper<overtime_statistics> ch = new ControllerHelper<overtime_statistics>();
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
        public ActionResult<common.response<overtime_statistics>> Get()
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
        public ActionResult<common.response> Post(overtime_statistics t)
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
        public ActionResult<common.response> Put(overtime_statistics t)
        {
            return Json(ch.Put(t));
        }
    }
}
