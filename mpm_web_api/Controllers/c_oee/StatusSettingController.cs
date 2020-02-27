using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.DAL.oee;
using mpm_web_api.model;
using Newtonsoft.Json;

namespace mpm_web_api.Controllers.oee
{
    [Produces(("application/json"))]
    [Route("api/v1/configuration/oee/status_setting")]
    [ApiController]
    public class StatusSettingController : SSOController
    {
        ControllerHelper<status_setting> ch = new ControllerHelper<status_setting>();


        [HttpDelete]
        public ActionResult<common.response<status_setting>> Delete(int id)
        {
            return Json(ch.Delete(id));
        }

        [HttpGet]
        public ActionResult<common.response<status_setting>> Get()
        {
            return Json(ch.Get());
        }

        [HttpPost]
        public ActionResult<common.response> Post(status_setting obj)
        {
            return Json(ch.Post(obj));
        }

        [HttpPut]
        public ActionResult<common.response> update(status_setting obj)
        {
            return Json(ch.Put(obj));
        }
    }
}