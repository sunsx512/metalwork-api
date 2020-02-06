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
    [Route("api/v1/configuration/work_order/virtual_line")]
    [SwaggerTag("设定虚拟线信息")]
    [ApiController]
    public class VirtualLineController : SSOController
    {

        ControllerHelper<virtual_line> ch = new ControllerHelper<virtual_line>();
        VirtualLineService vls = new VirtualLineService();
        [HttpDelete]
        public ActionResult<common.response> Delete(int id)
        {
            return Json(ch.Delete(id));
        }
        [HttpGet]
        public ActionResult<common.response<virtual_line_detail>> Get()
        {
            object obj;
            try
            {
                List < virtual_line_detail> lty = vls.QueryableToList();
                string strJson = JsonConvert.SerializeObject(lty);
                obj = common.ResponseStr<virtual_line_detail>((int)httpStatus.succes, "调用成功", lty);
            }
            catch (Exception ex)
            {
                obj = common.ResponseStr<virtual_line_detail>((int)httpStatus.serverError, ex.Message);
            }

            return Json(obj);
        }

        [HttpPost]
        public ActionResult<common.response> Post(virtual_line t)
        {
            return Json(ch.Post(t));
        }

        [HttpPut]
        public ActionResult<common.response> Put(virtual_line t)
        {
            return Json(ch.Put(t));
        }
    }
}