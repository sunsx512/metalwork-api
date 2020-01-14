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
    [Route("oee/status_setting")]
    [ApiController]
    public class status_setting_Controller : ControllerBase
    {
        status_setting_service service = new status_setting_service();

        [HttpGet]
        public ActionResult<string> Get()
        {
            List<status_setting> lty = service.GetList<status_setting>();
            string strJson = JsonConvert.SerializeObject(lty);
            return strJson;
        }

        [HttpGet("{status_name}")]
        public ActionResult<string> Get(string status_name)
        {
            List<status_setting> lty = service.GetList<status_setting>(status_name);
            string strJson = JsonConvert.SerializeObject(lty);
            return strJson;
        }

        [HttpPost]
        public ActionResult<bool> Post(status_setting obj)
        {
            bool res = service.insert<status_setting>(obj);
            return res;
        }

        [HttpPut]
        public ActionResult<bool> update(status_setting obj)
        {
            bool res = service.update<status_setting>(obj);
            return res;
        }
    }
}