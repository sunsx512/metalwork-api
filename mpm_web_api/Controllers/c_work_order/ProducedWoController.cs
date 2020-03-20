using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.DAL;
using mpm_web_api.model;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_work_order
{
    [ApiExplorerSettings(GroupName = "WorkOrder")]
    [Produces(("application/json"))]
    [Route("api/v1/configuration/work_order/produced_work_order")]
    [SwaggerTag("设定工单信息")]
    [ApiController]
    public class ProducedWoController : Controller
    {

        WoConfigService wcs = new WoConfigService();
        /// <summary>
        /// 获取已完结工单信息
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<wo_config_excute>> Getw()
        {
            object obj;
            //try
            //{
                List<wo_config_excute> lty = wcs.QueryableByStatus(3);
                string strJson = JsonConvert.SerializeObject(lty);
                obj = common.ResponseStr<wo_config_excute>((int)httpStatus.succes, "调用成功", lty);
            //}
            //catch (Exception ex)
            //{
            //    obj = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            //}
            return Json(obj);
        }
    }
}