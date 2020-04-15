using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.DAL.wo;
using mpm_web_api.model.m_wo;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_work_order
{
    [ApiExplorerSettings(GroupName = "WorkOrder")]
    [Produces(("application/json"))]
    [Route("api/v1/configuration/work_order/capacity_config")]
    [SwaggerTag("设定标准产能")]
    [ApiController]
    public class CapacityConfigController : Controller
    {

        CapacityConfigService ccs = new CapacityConfigService();

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
            object obj;
            if (ccs.Delete(id))
            {
                obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
            }
            else
            {
                obj = common.ResponseStr((int)httpStatus.serverError, "调用失败");
            }
            return Json(obj);
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<capacity_config>> Get()
        {
            object obj;
            List<capacity_config> lty = ccs.Get();
            obj = common.ResponseStr((int)httpStatus.succes, "调用成功", lty);
            return Json(obj);
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
        public ActionResult<common.response> Post(capacity_config t)
        {
            object obj;
            if (ccs.Post(t))
            {
                obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
            }
            else
            {
                obj = common.ResponseStr((int)httpStatus.serverError, "调用失败");
            }
            return Json(obj);
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
        public ActionResult<common.response> Put(capacity_config t)
        {
            object obj;
            if (ccs.Put(t))
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