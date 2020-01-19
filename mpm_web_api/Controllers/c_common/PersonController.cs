using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.model;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers
{
    [Produces(("application/json"))]
    [Route("api/v1/configuration/public/person")]
    [SwaggerTag("操作人员数据")]
    [ApiController]
    public class PersonController : SSOController
    {
        ControllerHelper<person> ch = new ControllerHelper<person>();
        /// <summary>
        /// 删除人员信息
        /// </summary>
        /// <param name="id">主键id</param>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpDelete]
        public ActionResult<common.response> Delete(int id)
        {
            return Json(ch.Delete(id));
        }

        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<common.response<person>> Get()
        {
            return  Json(ch.Get());
        }
        /// <summary>
        /// 新增人员
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPost]
        public ActionResult<common.response> Post(person t)
        {
            return Json(ch.Post(t));
        }
        /// <summary>
        /// 更新人员信息
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPut]
        public ActionResult<common.response> Put(person t)
        {
            return Json(ch.Put(t));
        }
    }
}