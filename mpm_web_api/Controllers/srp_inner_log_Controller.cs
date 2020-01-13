using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.DAL;
using mpm_web_api.model;
using Newtonsoft.Json;

namespace mpm_web_api.Controllers
{
    [Route("common/srp_inner_log")]
    [ApiController]
    public class srp_inner_log_Controller : ControllerBase
    {
        srp_inner_log_service service = new srp_inner_log_service();

        [HttpGet]
        public ActionResult<string> Get()
        {
            List<srp_inner_log> lty = service.GetList<srp_inner_log>();
            string strJson = JsonConvert.SerializeObject(lty);
            return strJson;
        }

        [HttpGet("{srp_code}")]
        public ActionResult<string> Get(string srp_code)
        {
            List<srp_inner_log> lty = service.GetList<srp_inner_log>(srp_code);
            string strJson = JsonConvert.SerializeObject(lty);
            return strJson;
        }

        [HttpPost]
        public ActionResult<bool> Post(srp_inner_log obj)
        {
            bool res = service.insert<srp_inner_log>(obj);
            return res;
        }

        [HttpPut]
        public ActionResult<bool> update(string srp_code)
        {
            bool res = service.update<srp_inner_log>(srp_code);
            return res;
        }
    }
}