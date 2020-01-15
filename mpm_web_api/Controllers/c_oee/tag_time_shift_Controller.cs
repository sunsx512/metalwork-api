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
    [Route("oee/tag_time_shift")]
    [ApiController]
    public class tag_time_shift_Controller : ControllerBase
    {
        tag_time_shift_service service = new tag_time_shift_service();

        [HttpGet]
        public ActionResult<string> Get()
        {
            List<tag_time_shift> lty = service.GetList<tag_time_shift>();
            string strJson = JsonConvert.SerializeObject(lty);
            return strJson;
        }

        [HttpGet("{machine_id}")]
        public ActionResult<string> Get(int machine_id)
        {
            List<tag_time_shift> lty = service.GetList<tag_time_shift>(machine_id);
            string strJson = JsonConvert.SerializeObject(lty);
            return strJson;
        }

        [HttpPost]
        public ActionResult<bool> Post(tag_time_shift obj)
        {
            bool res = service.insert<tag_time_shift>(obj);
            return res;
        }

        [HttpPut]
        public ActionResult<bool> update(tag_time_shift obj)
        {
            bool res = service.update<tag_time_shift>(obj);
            return res;
        }

        //[HttpDelete]
        //public ActionResult<string> Delete()
        //{
        //    return service.Delete();
        //}
    }
}