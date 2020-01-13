using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.DAL;
using mpm_web_api.model;
using Newtonsoft.Json;

namespace mpm_web_api.Controllers
{
    [Route("web/tag_type")]
    [ApiController]
    public class TagTypeController : SSOController
    {
        TagTypeService tt = new TagTypeService();
        [HttpGet]
        public ActionResult<string> Get()
        {
            List<tag_type> lty = tt.QueryableToList();
            string strJson = JsonConvert.SerializeObject(lty);
            string str = common.ResponseStr((int)httpStatus.succes, "调用成功", strJson);
            return str;
        }
    }
}