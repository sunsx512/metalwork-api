using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.DAL.oee;
using mpm_web_api.model.m_oee;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_oee
{
    [ApiExplorerSettings(GroupName = "OEE")]
    [Produces(("application/json"))]
    [Route("api/v1/configuration/oee/machine_lease_detail")]
    [SwaggerTag("租赁详细信息")]
    [ApiController]
    public class MachineLeaseDetailController : Controller
    {
        machine_lease_service mls = new machine_lease_service();
        /// <summary>
        /// 获取
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<machine_lease_detail>> Get()
        {

            object obj;
            //try
            //{
            List<machine_lease_detail> lty = mls.QueryableToList();
                string strJson = JsonConvert.SerializeObject(lty);
                obj = common.ResponseStr((int)httpStatus.succes, "调用成功", lty);
            //}
            //catch (Exception ex)
            //{
            //    obj = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            //}
            return Json(obj);
        }
    }
}