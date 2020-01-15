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
    [Route("oee/tricolor_tag_duration")]
    [ApiController]
    public class tricolor_tag_duration_Controller : ControllerBase
    {
        tricolor_tag_duration_service service = new tricolor_tag_duration_service();

        [HttpGet]
        public ActionResult<string> Get()
        {
            List<tricolor_tag_duration> lty = service.GetList<tricolor_tag_duration>();
            string strJson = JsonConvert.SerializeObject(lty);
            return strJson;
        }

        [HttpGet("{machine_id}")]
        public ActionResult<string> Get(int machine_id)
        {
            List<tricolor_tag_duration> lty = service.GetList<tricolor_tag_duration>(machine_id);
            string strJson = JsonConvert.SerializeObject(lty);
            return strJson;
        }

        [HttpPost]
        public ActionResult<bool> Post(tricolor_tag_duration obj)
        {
            bool res = service.insert<tricolor_tag_duration>(obj);
            return res;
        }

        [HttpPut]
        public ActionResult<bool> update(tricolor_tag_duration obj)
        {
            bool res = service.update<tricolor_tag_duration>(obj);
            return res;
        }

        //[HttpDelete]
        //public ActionResult<string> Delete()
        //{
        //    return service.Delete();
        //}
    }
}