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
    [ApiExplorerSettings(GroupName = "OEE")]
    [Produces(("application/json"))]
    [Route("api/v1/configuration/oee/utilization_formula")]
    [ApiController]
    public class UtilizationRateFormulaController : SSOController
    {
        ControllerHelper<utilization_rate_formula> ch = new ControllerHelper<utilization_rate_formula>();

        [HttpGet]
        public ActionResult<common.response<utilization_rate_formula>> Get()
        {
            return Json(ch.Get());
        }


        [HttpPut]
        public ActionResult<common.response> update(utilization_rate_formula obj)
        {
            return Json(ch.Put(obj));
        }

    }
}