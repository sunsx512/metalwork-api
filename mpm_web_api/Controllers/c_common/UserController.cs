using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.DAL;
using mpm_web_api.model.m_common;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_common
{
    [Produces(("application/json"))]
    [Route("api/v1/configuration/public/user")]
    [SwaggerTag("用户登录信息")]
    [ApiController]
    public class UserController : Controller
    {
        WisePaasUserService wpus = new WisePaasUserService();
        /// <summary>
        /// 新增或更新用户信息
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPost]
        public ActionResult<common.response> Post(wise_paas_user t)
        {
            object obj = common.ResponseStr((int)httpStatus.serverError, "调用失败"); ;
            if (wpus.SaveUserInfo(t))
            {
                obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
            }
            else
            {
                obj = common.ResponseStr((int)httpStatus.serverError, "调用失败");
            }
            return Json(obj);
        }
    }
}