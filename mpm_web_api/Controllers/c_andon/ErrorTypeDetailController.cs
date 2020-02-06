using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.model;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_andon
{
    [Produces(("application/json"))]
    [Route("/api/v1/configuration/andon/error_type_detail")]
    [SwaggerTag("异常类型详细类型")]
    [ApiController]
    public class ErrorTypeDetailController : SSOController, IController<error_type_details>
    {
        ControllerHelper<error_type_details> ch = new ControllerHelper<error_type_details>();

        [HttpDelete]
        public ActionResult<common.response> Delete(int id)
        {
            return Json(ch.Delete(id));
        }
        [HttpGet]
        public ActionResult<common.response<error_type_details>> Get()
        {
            return Json(ch.Get());
        }
        [HttpPost]
        public ActionResult<common.response> Post(error_type_details t)
        {
            return Json(ch.Post(t));
        }
        [HttpPut]
        public ActionResult<common.response> Put(error_type_details t)
        {
            return Json(ch.Put(t));
        }
    }
}