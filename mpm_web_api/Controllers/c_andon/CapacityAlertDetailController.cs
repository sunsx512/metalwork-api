using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.DAL.andon;
using mpm_web_api.model.m_error;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_andon
{
    [Produces(("application/json"))]
    [Route("/api/v1/configuration/andon/capacity_alert_detail")]
    [SwaggerTag("产能预警详细信息")]
    [ApiController]
    public class CapacityAlertDetailController : SSOController
    {
        CapacityAlertService cas = new CapacityAlertService();

        /// <summary>
        /// 获取详细信息
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<capacity_alert_detail>> Get()
        {
            object obj;
            try
            {
                List<capacity_alert_detail> lty = cas.QueryableDetailToList();
                string strJson = JsonConvert.SerializeObject(lty);
                obj = common.ResponseStr<capacity_alert_detail>((int)httpStatus.succes, "调用成功", lty);
            }
            catch (Exception ex)
            {
                obj = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            }

            return Json(obj);
        }
    }
}