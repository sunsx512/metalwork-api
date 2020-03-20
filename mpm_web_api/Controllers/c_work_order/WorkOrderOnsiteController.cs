using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.DAL.wo;
using mpm_web_api.model;
using mpm_web_api.model.m_wo;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_work_order
{
    [ApiExplorerSettings(GroupName = "WorkOrder")]
    [Produces(("application/json"))]
    [Route("api/v1/client/work_order/onsite")]
    [SwaggerTag("现场操作页面使用")]
    [ApiController]
    public class WorkOrderOnsiteController : Controller
    {
        OnsiteService os = new OnsiteService();
        /// <summary>
        /// 获取可以执行或者可以开启的工单号
        /// </summary>
        /// <param name="machine_id">设备id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<common.response<wo_config>> Get(int machine_id)
        {
            List<wo_config> lty = os.GetExecutableWo(machine_id);
            object obj = common.ResponseStr<wo_config>((int)httpStatus.succes, "调用成功", lty);
            return Json(obj);
        }

        /// <summary>
        /// 开始或者结束工单 
        /// </summary>
        /// <param name="type">0:开始工单 1:结束工单 2:暂停工单(未结)</param>
        /// <param name="machine_id"> 设备id</param>
        /// <param name="work_order_id"> 工单号</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPost("{type}")]
        public ActionResult<common.response> Post(int type, int machine_id,int work_order_id)
        {
            object obj = common.ResponseStr((int)httpStatus.serverError, "调用失败"); ;
            if(type == 0)
            {
                if(os.StartWorkOrder(machine_id, work_order_id))
                {
                    obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
                }
                else
                {
                    obj = common.ResponseStr((int)httpStatus.serverError, "调用失败");
                }
            }
            else if(type == 1)
            {
                if (os.FinishWorkOrder(machine_id, work_order_id))
                {
                    obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
                }
                else
                {
                    obj = common.ResponseStr((int)httpStatus.serverError, "调用失败");
                }
            }
            else if (type == 2)
            {
                if (os.SuspendWorkOrder(work_order_id))
                {
                    obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
                }
                else
                {
                    obj = common.ResponseStr((int)httpStatus.serverError, "调用失败");
                }
            }
            return Json(obj);
        }

        /// <summary>
        /// 修改当前工单的实际生产数量
        /// </summary>
        /// <param name="machine_id">设备号</param>
        /// <param name="work_order_id">工单号</param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<common.response> Put( int machine_id, int work_order_id,int count)
        {
            object obj ;
            if (os.modifyCount(machine_id, work_order_id,count))
            {
                obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
            }
            else
            {
                obj = common.ResponseStr((int)httpStatus.serverError, "调用失败");
            }
            return Json(obj);
        }

    }
}