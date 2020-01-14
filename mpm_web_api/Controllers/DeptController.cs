using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.DAL;
using mpm_web_api.model;

namespace mpm_web_api.Controllers
{
    [Route("api/v1/configuration/public/dept")]
    [ApiController]
    public class DeptController : SSOController, IController<department>
    {
        ControllerHelper<department> ch = new ControllerHelper<department>();
        [HttpDelete]
        public ActionResult<string> Delete(department t)
        {
            return ch.Delete(t);
        }
        [HttpGet]
        public ActionResult<string> Get()
        {
            return ch.Get();
        }
        [HttpPost]
        public ActionResult<string> Post(department t)
        {
            return ch.Post(t);
        }
        [HttpPut]
        public ActionResult<string> Put(department t)
        {
            return ch.Put(t);
        }
    }
}