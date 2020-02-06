using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.DAL;
using mpm_web_api.model;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_work_order
{
    [Produces(("application/json"))]
    [Route("api/v1/configuration/work_order/wo_config")]
    [SwaggerTag("设定工单信息")]
    [ApiController]
    public class WoConfigController : SSOController
    {
        ControllerHelper<wo_config> ch = new ControllerHelper<wo_config>();
        WoConfigService wcs = new WoConfigService();
        [HttpDelete]
        public ActionResult<common.response> Delete(int id)
        {
            return Json(ch.Delete(id));
        }
        [HttpGet]
        public ActionResult<common.response<wo_config_detail>> Get()
        {
            object obj;
            try
            {
                List<wo_config_detail> lty = wcs.QueryableToList();
                string strJson = JsonConvert.SerializeObject(lty);
                obj = common.ResponseStr<wo_config_detail>((int)httpStatus.succes, "调用成功", lty);
            }
            catch (Exception ex)
            {
                obj = common.ResponseStr<wo_config_detail>((int)httpStatus.serverError, ex.Message);
            }

            return Json(obj);
        }

        [HttpPost]
        public ActionResult<common.response> Post(wo_config t)
        {
            return Json(ch.Post(t));
        }

        [HttpPut]
        public ActionResult<common.response> Put(wo_config t)
        {
            return Json(ch.Put(t));
        }
    }
}