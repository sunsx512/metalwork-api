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

namespace mpm_web_api.Controllers.c_work_order
{
    [ApiExplorerSettings(GroupName = "WorkOrder")]
    [Produces(("application/json"))]
    [Route("api/v1/configuration/work_order/virtual_line")]
    [SwaggerTag("设定虚拟线信息")]
    [ApiController]
    public class VirtualLineController : SSOController
    {

        ControllerHelper<virtual_line> ch = new ControllerHelper<virtual_line>();
        ControllerHelper<wo_machine> chw = new ControllerHelper<wo_machine>();
        VirtualLineService vls = new VirtualLineService();
        /// <summary>
        /// 删除虚拟线
        /// </summary>
        /// <param name="id">id</param>
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
        /// 删除该虚拟线下的设备
        /// </summary>
        /// <param name="virtual_line_id">虚拟线id</param>
        /// <param name="machine_id">设备id</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpDelete("{virtual_line_id}")]
        public ActionResult<common.response> DeleteM(int virtual_line_id, int machine_id)
        {
            object obj;
            if(vls.DeleteByMachine(virtual_line_id, machine_id))
            {
                obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
            }
            else
            {
                obj = common.ResponseStr((int)httpStatus.succes, "删除失败");
            } 
            return Json(obj);
        }

        /// <summary>
        /// 获取详细的虚拟线信息
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<virtual_line_detail>> Get()
        {
            object obj;
            List < virtual_line_detail> lty = vls.QueryableToList();
            obj = common.ResponseStr<virtual_line_detail>((int)httpStatus.succes, "调用成功", lty);
            return Json(obj);
        }

        /// <summary>
        /// 新增虚拟线
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<common.response> Post(virtual_line t)
        {
            return Json(ch.Post(t));
        }
        /// <summary>
        /// 新增虚拟线下的设备
        /// </summary>
        /// <param name="virtual_line_id">虚拟线id</param>
        /// <param name="machine_id">设备id</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPost("{virtual_line_id}")]
        public ActionResult<common.response> PostM(int virtual_line_id, int machine_id)
        {
            wo_machine wm = new wo_machine();
            wm.machine_id = machine_id;
            wm.virtual_line_id = virtual_line_id;
            return Json(chw.Post(wm));
        }

        /// <summary>
        /// 更新虚拟线信息
        /// </summary>
        /// <param name="t"></param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPut]
        public ActionResult<common.response> Put(virtual_line t)
        {
            return Json(ch.Put(t));
        }
    }
}