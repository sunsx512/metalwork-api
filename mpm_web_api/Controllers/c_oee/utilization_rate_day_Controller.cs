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
    [Route("oee/utilization_rate_day")]
    [ApiController]
    public class utilization_rate_day_Controller : ControllerBase
    {
        utilization_rate_day_service service = new utilization_rate_day_service();
        [HttpGet("All")]
        public ActionResult<string> GetList()
        {
            List<utilization_rate_day> lty = service.GetList<utilization_rate_day>();
            string strJson = JsonConvert.SerializeObject(lty);
            return strJson;
        }

        [HttpGet("{machine_id}")]
        public ActionResult<string> GetList(int machine_id)
        {
            List<utilization_rate_day> lty = service.GetList<utilization_rate_day>(machine_id);
            string strJson = JsonConvert.SerializeObject(lty);
            return strJson;
        }



        [HttpPost]
        public ActionResult<bool> Post(utilization_rate_day obj)
        {
            bool res = service.insert<utilization_rate_day>(obj);
            return res;
        }

        [HttpPut("update")]
        public ActionResult<bool> update(utilization_rate_day obj)
        {
            bool res = service.update<utilization_rate_day>(obj);
            return res;
        }


        [HttpPut]
        public ActionResult<bool> updatetable(utilization_rate_day obj)
        {
            bool res = service.update<utilization_rate_day>(obj);
            return res;
        }
    }
}