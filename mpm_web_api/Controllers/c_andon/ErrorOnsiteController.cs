using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.DAL.andon;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_andon
{
    [ApiExplorerSettings(GroupName = "Andon")]
    [Produces(("application/json"))]
    [Route("api/v1/client/error/onsite")]
    [SwaggerTag("现场操作页面异常呼叫使用")]
    [ApiController]
    public class ErrorOnsiteController : Controller
    {
        ErrorOnsiteService eos = new ErrorOnsiteService();
        /// <summary>
        /// 触发异常 
        /// </summary>
        /// <param name="type">0:品质异常 1:设备异常 2:物料呼叫</param>
        /// <param name="machine_id"> 设备id</param>
        /// <param name="count">物料请求数量(仅物料呼叫需要填写)</param>
        /// <param name="material_name">物料名称(仅物料呼叫需要填写)</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPost("{type}")]
        public ActionResult<common.response> Post(int type,int machine_id,int count, string material_name)
        {
            object obj = common.ResponseStr((int)httpStatus.serverError, "调用失败"); ;
            if (type == 0)
            {
                if (eos.QualityTrigger(machine_id))
                {
                    obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
                }
                else
                {
                    obj = common.ResponseStr((int)httpStatus.serverError, "调用失败");
                }
            }
            else if (type == 1)
            {
                if (eos.EquipmentErrorTrigger(machine_id))
                {
                    obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
                }
                else
                {
                    obj = common.ResponseStr((int)httpStatus.serverError, "调用失败");
                }
            }
            else if (type == 2)
            {
                if (eos.MaterialRequestTrigger(machine_id,count,material_name))
                {
                    obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
                }
                else
                {
                    obj = common.ResponseStr((int)httpStatus.serverError, "调用失败");
                }
            }
            return Json(obj);
        }
        /// <summary>
        /// 异常签到
        /// </summary>
        /// <param name="type">0:品质异常 1:设备异常</param>
        /// <param name="machine_id">设备id</param>
        /// <param name="log_id">日志id</param>
        /// <param name="number">人员工号</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPut("{type}")]
        public ActionResult<common.response> Put(int type, int machine_id, int log_id, string number)
        {
            object obj = common.ResponseStr((int)httpStatus.serverError, "调用失败"); ;
            //try
            //{
                if (type == 0)
                {
                    if (eos.QualityConfirm(machine_id,log_id, number))
                    {
                        obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
                    }
                    else
                    {
                        obj = common.ResponseStr((int)httpStatus.serverError, "调用失败");
                    }
                }
                else if (type == 1)
                {
                    if (eos.EquipmentConfirm(machine_id, log_id, number))
                    {
                        obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
                    }
                    else
                    {
                        obj = common.ResponseStr((int)httpStatus.serverError, "调用失败");
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    obj = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            //}
            return Json(obj);
        }

        /// <summary>
        /// 异常解除
        /// </summary>
        /// <param name="type">0:品质异常 1:设备异常 2:物料</param>
        /// <param name="machine_id">设备id</param>
        /// <param name="log_id">日志id</param>
        /// <param name="count">不良数量(品质异常解除时填写)</param>
        /// <param name="error_type_id">异常类型(设备异常时填写)</param>
        /// <param name="error_type_detail_id">异常类型详细信息(设备异常时填写)</param>
        /// <returns></returns>
        [HttpDelete("{type}")]
        public ActionResult<common.response> Put(int type, int machine_id, int log_id, decimal count, int error_type_id, int error_type_detail_id)
        {
            object obj = common.ResponseStr((int)httpStatus.serverError, "调用失败"); ;
            //try
            //{
                if (type == 0)
                {
                    if (eos.Qualityrelease(machine_id, log_id, count))
                    {
                        obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
                    }
                    else
                    {
                        obj = common.ResponseStr((int)httpStatus.serverError, "调用失败");
                    }
                }
                else if (type == 1)
                {
                    if (eos.EquipmentRelease(machine_id, log_id, error_type_id, error_type_detail_id))
                    {
                        obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
                    }
                    else
                    {
                        obj = common.ResponseStr((int)httpStatus.serverError, "调用失败");
                    }
                }
                else if (type == 2)
                {
                    if (eos.MaterialRequestRelease(machine_id, log_id,""))
                    {
                        obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
                    }
                    else
                    {
                        obj = common.ResponseStr((int)httpStatus.serverError, "调用失败");
                    }
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