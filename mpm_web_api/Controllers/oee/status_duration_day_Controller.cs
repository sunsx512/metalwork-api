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
    [Route("api/[controller]")]
    [ApiController]
    public class status_duration_day_Controller : ControllerBase
    {
        status_duration_day_service service = new status_duration_day_service();

        [HttpGet]
        public ActionResult<string> Get()
        {
            List<status_duration_day> lty = service.GetList<status_duration_day>();
            string strJson = JsonConvert.SerializeObject(lty);
            return strJson;
        }

        [HttpGet("{machine_id}")]
        public ActionResult<string> Get(int machine_id)
        {
            List<status_duration_day> lty = service.GetList<status_duration_day>(machine_id);
            string strJson = JsonConvert.SerializeObject(lty);
            return strJson;
        }

        [HttpPost]
        public ActionResult<bool> Post(status_duration_day obj)
        {
            bool res = service.insert<status_duration_day>(obj);
            return res;
        }

        [HttpPut]
        public ActionResult<bool> update(status_duration_day obj)
        {
            bool res = service.update<status_duration_day>(obj);
            return res;
        }

        //[HttpDelete]
        //public ActionResult<string> Delete()
        //{
        //    return service.Delete();
        //}
    }
}