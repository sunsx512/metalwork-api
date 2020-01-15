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
    [Route("api/status_duration_order")]
    [ApiController]
    public class status_duration_order_Controller : ControllerBase
    {
        status_duration_order_service service = new status_duration_order_service();

        [HttpGet]
        public ActionResult<string> Get()
        {
            List<status_duration_order> lty = service.GetList<status_duration_order>();
            string strJson = JsonConvert.SerializeObject(lty);
            return strJson;
        }

        [HttpGet("{machine_id}")]
        public ActionResult<string> Get(int machine_id)
        {
            List<status_duration_order> lty = service.GetList<status_duration_order>(machine_id);
            string strJson = JsonConvert.SerializeObject(lty);
            return strJson;
        }

        [HttpPost]
        public ActionResult<bool> Post(status_duration_order obj)
        {
            bool res = service.insert<status_duration_order>(obj);
            return res;
        }

        [HttpPut]
        public ActionResult<bool> update(status_duration_order obj)
        {
            bool res = service.update<status_duration_order>(obj);
            return res;
        }

        //[HttpDelete]
        //public ActionResult<string> Delete()
        //{
        //    return service.Delete();
        //}
    }
}