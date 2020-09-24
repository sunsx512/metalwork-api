using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.DAL.andon;
using mpm_web_api.model;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_andon
{
    [ApiExplorerSettings(GroupName = "Andon")]
    [Produces(("application/json"))]
    [Route("/api/v1/configuration/andon/error_log_detail")]
    [SwaggerTag("详细异常日志")]
    [ApiController]
    public class ErrorLogDetailController : Controller
    {
        ErrorLogService els = new ErrorLogService();
        /// <summary>
        /// 按状态获取详细异常日志信息
        /// </summary>
        /// <param name="status">状态分为 0:所有异常日志 1:等待处理的异常日志 2:正在处理的异常日志 3:已处理的异常日志 四种</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<error_log_detail>> Get(int status)
        {
            object obj;
            //try
            //{
                List<error_log_detail> lty = els.QueryableDetailToListByStatus(status);
                string strJson = JsonConvert.SerializeObject(lty);
                obj = common.ResponseStr<error_log_detail>((int)httpStatus.succes, "调用成功", lty);
            //}
            //catch (Exception ex)
            //{
            //    obj = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            //}
            return Json(obj);
        }


        /// <summary>
        /// 按异常类型和状态获取详细异常日志信息
        /// </summary>
        /// <param name="type">异常的类型 0:设备类异常 1:品质类异常 2.物料请求类异常 </param>
        /// <param name="status">状态分为 0:所有异常日志 1:等待处理的异常日志 2:正在处理的异常日志 3:已处理的异常日志 四种</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet("{type}")]
        public ActionResult<common.response<error_log_detail>> Get(int type, int status)
        {
            object obj;
            //try
            //{
                List<error_log_detail> lty = els.QueryableDetailToListByStatus(type, status);
                string strJson = JsonConvert.SerializeObject(lty);
                obj = common.ResponseStr<error_log_detail>((int)httpStatus.succes, "调用成功", lty);
            //}
            //catch (Exception ex)
            //{
            //    obj = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            //}
            return Json(obj);
        }
    }
}