using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.model;
using mpm_web_api.model.m_common;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_common
{
    [ApiExplorerSettings(GroupName = "Common")]
    [Produces(("application/json"))]
    [Route("api/v1/configuration/public/utilizationlimit")]
    [SwaggerTag("稼动率生效范围")]
    [ApiController]
    public class UtilizationLimitController : Controller, IController<UtilizationLimit>
    {
        ControllerHelper<UtilizationLimit> ch = new ControllerHelper<UtilizationLimit>();

        [HttpDelete]
        public ActionResult<common.response> Delete(int id)
        {
            return Json(ch.Delete(id));
        }

        [HttpGet]
        public ActionResult<common.response<UtilizationLimit>> Get()
        {
            return Json(ch.Get());
        }

        [HttpPost]
        public ActionResult<common.response> Post(UtilizationLimit t)
        {
            return Json(ch.Post(t));
        }

        [HttpPut]
        public ActionResult<common.response> Put(UtilizationLimit t)
        {
            return Json(ch.Put(t));
        }
    }
}
