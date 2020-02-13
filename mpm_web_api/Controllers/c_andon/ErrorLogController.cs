using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.DAL.andon;
using mpm_web_api.model;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_andon
{
    [Produces(("application/json"))]
    [Route("/api/v1/configuration/andon/error_log")]
    [SwaggerTag("异常日志")]
    [ApiController]
    public class ErrorLogController : SSOController
    {
        ControllerHelper<error_log> ch = new ControllerHelper<error_log>();
        ErrorLogService els = new ErrorLogService();
        //[HttpDelete]   
        //public ActionResult<common.response> Delete(int id)
        //{
        //    return Json(ch.Delete(id));
        //}

        /// <summary>
        /// 获取工单信息
        /// </summary>
        /// <param name="status">状态分为 all penging processing finished 四种</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<error_log>> Get(string status)
        {
            object obj;
            try
            {
                List<error_log> lty = els.QueryableToListByStatus(status);
                string strJson = JsonConvert.SerializeObject(lty);
                obj = common.ResponseStr<error_log>((int)httpStatus.succes, "调用成功", lty);
            }
            catch (Exception ex)
            {
                obj = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            }
            return Json(obj);
        }

        /// <summary>
        /// 更新异常信息
        /// </summary>
        /// <param name="t"></param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPut]
        public ActionResult<common.response> Put(error_log t)
        {
            return Json(ch.Put(t));
        }
    }
}