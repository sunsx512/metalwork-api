using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.DAL;
using mpm_web_api.model;
using Newtonsoft.Json;

namespace mpm_web_api.Controllers.andon
{
    //[Route("andon/error_type_details")]
    //[ApiController]
    //public class error_type_details_Controller : ControllerBase
    //{
    //    BaseService bs = new BaseService();

    //    [HttpPost]
    //    public ActionResult<bool> Post(error_type_details obj)
    //    {
    //        bool res = bs.insert<error_type_details>(obj);
    //        return res;
    //    }

    //    [HttpPut()]
    //    public ActionResult<bool> update(error_type_details obj)
    //    {
    //      //  bool res = bs.update<error_type_details>(obj,"id");
    //        return true;
    //    }
    //}
}