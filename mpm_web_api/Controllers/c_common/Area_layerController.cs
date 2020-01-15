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
        public ActionResult<common.response<area_layer>> Get()
        {
            return Json(ch.Get());
        }
        /// <summary>
        /// 新增Area_layer
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPost]
        public ActionResult<common.response> Post(area_layer t)
        {
            return Json(ch.Post(t));
        }
        /// <summary>
        /// 更新Area_layer
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPut]
        public ActionResult<common.response> Put(area_layer t)
        {
            return Json(ch.Put(t));
        }
        /// <summary>
        /// 删除Area_layer
        /// </summary>
        /// <param name="id">主键id</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpDelete]
        public ActionResult<common.response> Delete(int id)
        {
            return Json(ch.Delete(id));
        }
    }
}