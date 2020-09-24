using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.DAL.ehs;
using mpm_web_api.model.m_ehs;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.Controllers.c_ehs
{
    [ApiExplorerSettings(GroupName = "EHS")]
    [Produces(("application/json"))]
    [Route("api/v1/configuration/ehs/env_standard")]
    [SwaggerTag("环境标准配置")]
    [ApiController]
    public class EnvStandardController: Microsoft.AspNetCore.Mvc.Controller
    {
        EnvStandardService ess = new EnvStandardService();
        ControllerHelper<standard> ch = new ControllerHelper<standard>();
        /// <summary>
        /// 获取环境标准配置信息
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<standard_detail>> Get()
        {
            object obj;
            List<standard_detail> lty = ess.QueryableToList();
            obj = common.ResponseStr<standard_detail>((int)httpStatus.succes, "调用成功", lty);
            return Json(obj);
        }


        /// <summary>
        /// 删除部门
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
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPost]
        public ActionResult<common.response> Post(standard t)
        {
            return Json(ch.Post(t));
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPut]
        public ActionResult<common.response> Put(standard t)
        {
            return Json(ch.Put(t));
        }
    }
}
