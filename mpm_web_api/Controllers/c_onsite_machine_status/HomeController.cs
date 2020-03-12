using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnsiteStatusWorker.Models;

namespace OnsiteStatusWorker.Controllers
{
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
