using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace mpm_web_api.Controllers
{
    [Produces(("application/json"))]
    [Route("error")]
    [ApiController]
    public class ErrorController : Controller
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "401 No Authorization";
        }
    }
}