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
    [ApiExplorerSettings(GroupName = "Andon")]
    [Produces(("application/json"))]
    [Route("/api/v1/configuration/andon/machine_cost_alert_detail")]
    [SwaggerTag("设备费用预警详细信息")]
    [ApiController]
    public class MachineCostAlertDetailController : Controller
    {
        MachineCostAlertService mcas = new MachineCostAlertService();
        /// <summary>
        /// 获取详细信息
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<machine_cost_alert_detail>> Get()
        {
            object obj;
            //try
            //{
                List<machine_cost_alert_detail> lty = mcas.QueryableDetailToList();
                string strJson = JsonConvert.SerializeObject(lty);
                obj = common.ResponseStr<machine_cost_alert_detail>((int)httpStatus.succes, "调用成功", lty);
            //}
            //catch (Exception ex)
            //{
            //    obj = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            //}

            return Json(obj);
        }
    }
}