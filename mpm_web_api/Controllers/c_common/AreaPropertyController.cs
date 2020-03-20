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
/// </summary>
namespace mpm_web_api.Controllers.c_common
{
    [ApiExplorerSettings(GroupName = "Common")]
    [Produces(("application/json"))]
    [Route("api/v1/configuration/public/area_property")]
    [SwaggerTag("区域节点属性(班别，固定排休，非固定排休)")]
    [ApiController]
    public class AreaPropertyController : SSOController
    {
        ControllerHelper<area_property> ch = new ControllerHelper<area_property>();
        AreaPropertyService aps = new AreaPropertyService();


        /// <summary>
        /// 获取所有节点属性信息
        /// </summary>
        /// <param name="type">对应四种种类别 shift:班别  unfixed_break:非固定排休 fixed_break:固定排休 time_zone:时区</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet("{type}")]
        public ActionResult<common.response<area_property>> Get(string type)
        {
            object obj;
            //try
            //{
                List<area_property> lty;
                switch (type)
                {
                    case "shift": lty = aps.QueryShift(); break;
                    case "unfixed_break": lty = aps.QueryUnfixedBreak(); break;
                    case "fixed_break": lty = aps.QueryFixedBreak(); break;
                    case "time_zone": lty = aps.QueryTimeZone(); break;
                    default: return Json(ch.Get());
                }
                string strJson = JsonConvert.SerializeObject(lty);
                obj = common.ResponseStr((int)httpStatus.succes, "调用成功", lty);
            //}
            //catch (Exception ex)
            //{
            //    obj = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            //}

            return Json(obj);
            
        }


        /// <summary>
        /// 新增区域节点属性(排休，班别，非固定排休)
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        /// <remarks>
        /// format字段说明
        /// 如果是班别填入格式如下  name_cn :shift
        /// {"day":{"start":"8:00:00","end":"16:00:00"},"night":{"start":"16:00:00","end":"8:00:00"}}
        /// 固定排休格式: name_cn :fixed_break
        /// {"rest":[{"start":"8:00:00","end":"16:00:00"},{"start":"8:00:00","end":"16:00:00"}]}
        /// 非固定排休格式: name_cn :unfixed_break
        /// {"rest":[{"start":"2019-11-12 8:00:00","end":"2019-11-12 16:00:00"},{"start":"2019-11-12 8:00:00","end":"2019-11-12 16:00:00"}]}
        /// 时区: 8    (直接填数字)
        /// 注意:format为json格式数据 一定要满足以上格式要求，不然后端解析会出错
        /// </remarks>
        [HttpPost]
        public ActionResult<common.response> Post(area_property t)
        {
            return Json(ch.Post(t));
        }



        /// <summary>
        /// 更新Area_Property
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPut]
        public ActionResult<common.response> Put(area_property t)
        {
            return Json(ch.Put(t));
        }
        /// <summary>
        /// 删除Area_Property
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