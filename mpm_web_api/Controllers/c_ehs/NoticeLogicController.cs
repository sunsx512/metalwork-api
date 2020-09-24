using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.DAL.ehs;
using mpm_web_api.model.m_ehs;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_ehs
{
    [ApiExplorerSettings(GroupName = "EHS")]
    [Produces(("application/json"))]
    [Route("api/v1/configuration/ehs/notice_logic")]
    [SwaggerTag("触发通知人员")]
    [ApiController]
    public class NoticeLogicController : Microsoft.AspNetCore.Mvc.Controller
    {
        ControllerHelper<notice_logic> ch = new ControllerHelper<notice_logic>();
        NoticeService ns = new NoticeService();
        /// <summary>
        /// 删除
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
        /// 获取
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<common.response<notice_logic_detail>> Get()
        {
            object obj;
            List<notice_logic_detail> lty = ns.GetNoticeLogicDetail();
            string strJson = JsonConvert.SerializeObject(lty);
            obj = common.ResponseStr((int)httpStatus.succes, "调用成功", lty);
            return Json(obj);
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
        public ActionResult<common.response> Post(notice_logic t)
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
        public ActionResult<common.response> Put(notice_logic t)
        {
            return Json(ch.Put(t));
        }
    }
}
