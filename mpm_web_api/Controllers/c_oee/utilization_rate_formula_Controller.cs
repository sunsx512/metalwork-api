using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.DAL.oee;
using mpm_web_api.model;
using Newtonsoft.Json;


namespace mpm_web_api.Controllers.oee
{
    [Route("api/v1/configuration/oee/utilization_formula")]
    [ApiController]
    public class utilization_rate_formula_Controller : ControllerBase
    {
         utilization_rate_formula_service service = new utilization_rate_formula_service();

        [HttpGet]
        public ActionResult<string> Get()
        {
            List<utilization_rate_formula> lty = service.GetList<utilization_rate_formula>();
            string strJson = JsonConvert.SerializeObject(lty);
            return strJson;
        }


        [HttpPost]
        public ActionResult<bool> Post(utilization_rate_formula obj)
        {
            bool res = service.insert<utilization_rate_formula>(obj);
            return res;
        }

        [HttpPut]
        public ActionResult<bool> update(utilization_rate_formula obj)
        {
            bool res = service.update<utilization_rate_formula>(obj);
            return res;
        }

    }
}