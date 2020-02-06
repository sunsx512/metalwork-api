using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.model;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_work_order
{
    [Produces(("application/json"))]
    [Route("api/v1/configuration/work_order/wo_machine")]
    [SwaggerTag("设定工单信息")]
    [ApiController]
    public class WoMachineController : SSOController, IController<wo_machine>
    {
        ControllerHelper<wo_machine> ch = new ControllerHelper<wo_machine>();
        [HttpDelete]
        public ActionResult<common.response> Delete(int id)
        {
            return Json(ch.Delete(id));
        }
        [HttpGet]
        public ActionResult<common.response<wo_machine>> Get()
        {
            return Json(ch.Get());
        }
        [HttpPost]
        public ActionResult<common.response> Post(wo_machine t)
        {
            return Json(ch.Post(t));
        }
        [HttpPut]
        public ActionResult<common.response> Put(wo_machine t)
        {
            return Json(ch.Put(t));
        }
    }
}