using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.model;

namespace mpm_web_api.Controllers
{
    [Produces(("application/json"))]
    [Route("api/v1/configuration/public/tag_type_sub")]
    [ApiController]
    public class TagTypeSubController : SSOController, IController<tag_type_sub>
    {
        ControllerHelper<tag_type_sub> ch = new ControllerHelper<tag_type_sub>();
        [HttpDelete]
        public ActionResult<common.response> Delete(int id)
        {
            return Json(ch.Delete(id));
        }
        [HttpGet]
        public ActionResult<common.response<tag_type_sub>> Get()
        {
            return Json(ch.Get());
        }

        [HttpPost]
        public ActionResult<common.response> Post(tag_type_sub t)
        {
            return Json(ch.Post(t));
        }
        [HttpPut]
        public ActionResult<common.response> Put(tag_type_sub t)
        {
            return Json(ch.Put(t));
        }
    }
}