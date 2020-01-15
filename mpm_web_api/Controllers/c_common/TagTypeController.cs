using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.DAL;
using mpm_web_api.model;
using Newtonsoft.Json;

namespace mpm_web_api.Controllers
{
    [Produces(("application/json"))]
    [Route("api/v1/configuration/public/tag_type")]
    [ApiController]
    public class TagTypeController : SSOController, IController<tag_type>
    {
        ControllerHelper<tag_type> ch = new ControllerHelper<tag_type>();

        [HttpDelete]
        public ActionResult<common.response> Delete(int id)
        {
            return Json(ch.Delete(id));
        }
        [HttpGet]
        public ActionResult<common.response<tag_type>> Get()
        {
            return Json(ch.Get());
        }

        [HttpPost]
        public ActionResult<common.response> Post(tag_type t)
        {
            return Json(ch.Post(t));
        }
        [HttpPut]
        public ActionResult<common.response> Put(tag_type t)
        {
            return Json(ch.Put(t));
        }
    }
}