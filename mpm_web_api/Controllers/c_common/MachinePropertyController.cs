using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.DAL;
using mpm_web_api.model.m_common;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_common
{
    [ApiExplorerSettings(GroupName = "Common")]
    [Produces(("application/json"))]
    [Route("api/v1/configuration/public/machine_property")]
    [SwaggerTag("设定设备的属性(班别，固定排休，非固定排休)")]
    [ApiController]
    public class MachinePropertyController : Controller
    {
        ControllerHelper<machine_property> ch = new ControllerHelper<machine_property>();
        MachinePropertyService mps = new MachinePropertyService();



        [HttpGet("{type}")]
        public ActionResult<common.response<machine_property>> Get(string type)
        {
            object obj;
            List<machine_property> lty;
            ch.Get();
            switch (type)
            {
                case "shift": lty = mps.QueryShift(); break;
                case "unfixed_break": lty = mps.QueryUnfixedBreak(); break;
                case "fixed_break": lty = mps.QueryFixedBreak(); break;
                case "time_zone": lty = mps.QueryTimeZone(); break;
                default: return Json(ch.Get());
            }
            string strJson = JsonConvert.SerializeObject(lty);
            obj = common.ResponseStr((int)httpStatus.succes, "调用成功", lty);
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
        public ActionResult<common.response> Post(machine_property t)
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
        public ActionResult<common.response> Put(machine_property t)
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
