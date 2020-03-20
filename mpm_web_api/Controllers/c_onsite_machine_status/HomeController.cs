using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using OnsiteStatusWorker.Models;

namespace OnsiteStatusWorker.Controllers
{
    [ApiExplorerSettings(GroupName = "Dashboard")]
    [Route("api/")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "连接成功";
        }

    }
}
