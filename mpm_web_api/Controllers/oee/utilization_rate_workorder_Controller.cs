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
    [Route("oee/utilization_rate_workorder")]
    [ApiController]
    public class utilization_rate_workorder_Controller : ControllerBase
    {
        utilization_rate_workorder_service service = new utilization_rate_workorder_service();
        [HttpGet("All")]
        public ActionResult<string> GetList()
        {
            List<utilization_rate_workorder> lty = service.GetList<utilization_rate_workorder>();
            string strJson = JsonConvert.SerializeObject(lty);
            return strJson;
        }

        [HttpGet]
        public ActionResult<string> GetList(utilization_rate_workorder obj)
        {
            List<utilization_rate_workorder> lty = service.GetList<utilization_rate_workorder>(obj);
            string strJson = JsonConvert.SerializeObject(lty);
            return strJson;
        }



        [HttpPost]
        public ActionResult<bool> Post(utilization_rate_workorder obj)
        {
            bool res = service.insert<utilization_rate_workorder>(obj);
            return res;
        }

        [HttpPut]
        public ActionResult<bool> update(utilization_rate_workorder obj)
        {
            bool res = service.update<utilization_rate_workorder>(obj);
            return res;
        }


        //[HttpPut]
        //public ActionResult<bool> updatetable(utilization_rate_workorder obj)
        //{
        //    bool res = service.update<utilization_rate_workorder>(obj);
        //    return res;
        //}
    }
}