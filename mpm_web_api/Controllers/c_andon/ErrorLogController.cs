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
    [ApiExplorerSettings(GroupName = "Andon")]
    [Produces(("application/json"))]
    [Route("/api/v1/configuration/andon/error_log")]
    [SwaggerTag("异常日志")]
    [ApiController]
    public class ErrorLogController : SSOController
    {
        ControllerHelper<error_log> ch = new ControllerHelper<error_log>();
        ErrorLogService els = new ErrorLogService();

        /// <summary>
        /// 按状态获取异常日志信息
        /// </summary>
        /// <param name="status">状态分为 0:所有异常日志 1:等待处理的异常日志 2:正在处理的异常日志 3:已处理的异常日志 四种</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<error_log>> Get(int status)
        {
            object obj;
            //try
            //{
                List<error_log> lty = els.QueryableToListByStatus(status);
                string strJson = JsonConvert.SerializeObject(lty);
                obj = common.ResponseStr<error_log>((int)httpStatus.succes, "调用成功", lty);
            //}
            //catch (Exception ex)
            //{
            //    obj = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            //}
            return Json(obj);
        }


        /// <summary>
        /// 按异常类型和状态获取异常日志信息
        /// </summary>
        /// <param name="type">异常的类型 0:设备类异常 1:品质类异常 2.物料请求类异常 </param>
        /// <param name="status">状态分为 0:所有异常日志 1:等待处理的异常日志 2:正在处理的异常日志 3:已处理的异常日志 四种</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet("{type}")]
        public ActionResult<common.response<error_log>> Get(int type, int status)
        {
            object obj;
            //try
            //{
                List<error_log> lty = els.QueryableToListByStatusAndType(type,status);
                string strJson = JsonConvert.SerializeObject(lty);
                obj = common.ResponseStr<error_log>((int)httpStatus.succes, "调用成功", lty);
            //}
            //catch (Exception ex)
            //{
            //    obj = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            //}
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

        /// <summary>
        /// 更新替代者的名字
        /// </summary>
        /// <param name="id">异常日志id</param>
        /// <param name="name">替代者名字</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPut("{id}")]
        public ActionResult<common.response> Put(int id, string name)
        {
            object obj;
            //try
            //{
                if (els.UpdataSubstitutes(id, name))
                {
                    obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
                }
                else
                {
                    obj = common.ResponseStr((int)httpStatus.dbError, "添加失败");
                }
            //}
            //catch (Exception ex)
            //{
            //    obj = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            //}
            return Json(obj);
        }
    }
}