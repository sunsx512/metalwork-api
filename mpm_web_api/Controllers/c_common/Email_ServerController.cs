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
/// Email_Server控制器
/// </summary>
namespace mpm_web_api.Controllers.c_common
{
    [Produces(("application/json"))]
    [Route("api/v1/configuration/public/email_server")]
    [SwaggerTag("邮件服务")]
    [ApiController]
    public class Email_ServerController : SSOController
    {
        ControllerHelper<email_server> ch = new ControllerHelper<email_server>();

        /// <summary>
        /// 获取所有邮件服务器信息
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<email_server>> Get()
        {
            return Json(ch.Get());
        }

        /// <summary>
        /// 更新邮件服务器信息
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPut]
        public ActionResult<common.response> Put(email_server t)
        {
            return Json(ch.Put(t));
        }

    }
}