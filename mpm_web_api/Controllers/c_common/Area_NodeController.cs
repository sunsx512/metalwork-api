using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.DAL;
using mpm_web_api.model;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

/// <summary>
/// add by sunsx.sun
/// 
/// Area_Node控制器
/// </summary>
namespace mpm_web_api.Controllers.c_common
{
    [Produces(("application/json"))]
    [Route("api/v1/configuration/public/area_node")]
    [SwaggerTag("区域节点")]
    [ApiController]
    public class Area_NodeController : SSOController,IController<area_node>
    {
        ControllerHelper<area_node> ch = new ControllerHelper<area_node>();
        AreaNodeService ans = new AreaNodeService();
        /// <summary>
        /// 获取所有区域节点信息
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<area_node>> Get()
        {
            return Json(ch.Get());
        }

        /// <summary>
        /// 获取所有区域属性详细信息
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet("{detail}")]
        public ActionResult<common.response<area_node>> Get(string detail)
        {
            if(detail == "detail")
            {
                object obj;
                try
                {
                    List<area_node_detail> lty = ans.QueryableToList();
                    string strJson = JsonConvert.SerializeObject(lty);
                    obj = common.ResponseStr<area_node_detail>((int)httpStatus.succes, "调用成功", lty);
                }
                catch (Exception ex)
                {
                    obj = common.ResponseStr((int)httpStatus.serverError, ex.Message);
                }

                return Json(obj);
            }
            else
            {
                 return Json(""); ;
            }
        }
        /// <summary>
        /// 新增Area_Node
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPost]
        public ActionResult<common.response> Post(area_node t)
        {
            return Json(ch.Post(t));
        }
        /// <summary>
        /// 更新Area_Node
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPut]
        public ActionResult<common.response> Put(area_node t)
        {
            return Json(ch.Put(t));
        }
        /// <summary>
        /// 删除Area_Node
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