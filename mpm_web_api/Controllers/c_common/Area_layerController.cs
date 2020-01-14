using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.model;

/// <summary>
/// add by sunsx.sun
/// 
/// Area_layer控制器
/// </summary>
namespace mpm_web_api.Controllers.c_common
{
    [Produces(("application/json"))]
    [Route("api/v1/configuration/public/area_layer")]
    [ApiController]
    public class Area_layerController : SSOController,IController<area_layer>
    {
        ControllerHelper<area_layer> ch = new ControllerHelper<area_layer>();

        /// <summary>
        /// 获取所有Area_layer信息
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Json(ch.Get());
        }

        public ActionResult<string> Post(area_layer t)
        {
            throw new NotImplementedException();
        }

        public ActionResult<string> Put(area_layer t)
        {
            throw new NotImplementedException();
        }

        public ActionResult<string> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}