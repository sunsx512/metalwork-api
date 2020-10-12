using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.DAL;
using mpm_web_api.model.m_common;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_common
{
    [ApiExplorerSettings(GroupName = "Common")]
    [Produces(("application/json"))]
    [Route("api/v1/configuration/public/Client")]
    [SwaggerTag("获取云端Client信息,用于登录认证")]
    [ApiController]
    public class ClientController : Controller
    {
        ClientService cs = new ClientService();
        /// <summary>
        /// 获取client信息
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<client>> Get(string serviceName, string cluster, string workspace, string @namespace, string datacenter)
        {
            List<client> list = new List<client>();
            list.Add(cs.QuerytoSingle());
            var obj = common.ResponseStr((int)httpStatus.succes, "调用成功", list);
            return Json(obj);
        }

        /// <summary>
        /// 新增client信息
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPost]
        public ActionResult<common.response> Post(client client)
        {
            object obj;
            if (cs.Save(client))
                obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
            else
                obj = common.ResponseStr((int)httpStatus.clientError, "调用失败");
            return Json(obj);
        }
    }
}
