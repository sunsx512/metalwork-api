using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.DAL;
using mpm_web_api.model;
using Newtonsoft.Json;

namespace mpm_web_api.Controllers.andon
{
    //[Route("andon/error_type")]
    //[ApiController]
    //public class error_type_Controller : ControllerBase
    //{
    //    BaseService bs = new BaseService();

    //    [HttpPost]
    //    public ActionResult<bool> Post(error_type obj)
    //    {
    //        bool res = bs.insert<error_type>(obj);
    //        return res;
    //    }

    //    [HttpPut("update")]
    //    public ActionResult<bool> update(error_type obj)
    //    {          
    //        bool  res = bs.update<error_type>(obj);
    //        return res;
    //    }

    //    error_typeService et = new error_typeService();

    //    [HttpPut("updatetable")]
    //    public ActionResult<bool> updatetable(string value,int id)
    //    {
    //        bool res = et.update<error_type>(value,id);
    //        return res;
    //    }
    //}
}