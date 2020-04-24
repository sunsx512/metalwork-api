using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.DAL.andon;
using mpm_web_api.model.m_error;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_andon
{
    [ApiExplorerSettings(GroupName = "Andon")]
    [Produces(("application/json"))]
    [Route("/api/v1/configuration/andon/andon_logic_detail")]
    [SwaggerTag("详细安灯逻辑信息")]
    [ApiController]
    public class AndonLogicDetailController : Controller
    {

        AndonLogicService als = new AndonLogicService();
        /// <summary>
        /// 查询
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<andon_logic_detail>> Get()
        {           
            object obj;
            List<andon_logic_detail> lty = als.QueryableDetailToList();
            obj = common.ResponseStr<andon_logic_detail>((int)httpStatus.succes, "调用成功", lty);
            return Json(obj);
        }
    }
}