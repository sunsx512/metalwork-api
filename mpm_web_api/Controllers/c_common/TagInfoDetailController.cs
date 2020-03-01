using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.DAL;
using mpm_web_api.model;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using static mpm_web_api.common;

namespace mpm_web_api.Controllers.c_common
{
    [Produces(("application/json"))]
    [Route("api/v1/configuration/public/tag_detail")]
    [SwaggerTag("标签详细信息")]
    [ApiController]
    public class TagInfoDetailController : Controller
    {
        TagService ts = new TagService();

        /// <summary>
        /// 获取所有标签信息
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<tag_info_detail>> Get()
        {
            object obj;
            //try
            //{
                List<tag_info_detail> lty = ts.QueryableToList();
                string strJson = JsonConvert.SerializeObject(lty);
                obj = common.ResponseStr<tag_info_detail>((int)httpStatus.succes, "调用成功", lty);
            //}
            //catch (Exception ex)
            //{
            //    obj = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            //}

            return Json(obj);
        }
    }
    
}