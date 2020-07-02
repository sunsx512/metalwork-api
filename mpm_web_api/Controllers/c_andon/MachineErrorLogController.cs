using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.DAL.andon;
using mpm_web_api.model;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_andon
{
    [ApiExplorerSettings(GroupName = "Andon")]
    [Produces(("application/json"))]
    [Route("/api/v1/configuration/andon/error_log/machine")]
    [SwaggerTag("设备异常日志")]
    [ApiController]
    public class MachineErrorLogController : SSOController
    {
        ControllerHelper<error_log> ch = new ControllerHelper<error_log>();
        ErrorLogService els = new ErrorLogService();

        /// <summary>
        /// 按设备名及工单号查询异常日志
        /// </summary>
        /// <param name="machine"> 设备名</param>
        /// <param name="work_order">工单号</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<error_log>> Get(string machine,string work_order)
        {
            object obj;
            //try
            //{
                List<error_log> lty = els.QueryableToListByMahcine(machine, work_order);
                string strJson = JsonConvert.SerializeObject(lty);
                obj = common.ResponseStr((int)httpStatus.succes, "调用成功", lty);
            //}
            //catch (Exception ex)
            //{
            //    obj = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            //}
            return Json(obj);
        }

        /// <summary>
        /// 添加异常信息
        /// </summary>
        /// <param name="config_id">异常配置id</param>
        /// <param name="error_name">异常名称</param>
        /// <param name="machine_name">设备名</param>
        /// <param name="responsible_name">责任人</param>
        /// <param name="work_order">工单</param>
        /// <param name="part_number">机种</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPost]
        public ActionResult<common.response> Post(int config_id,string error_name,string machine_name,int responsible_id,string work_order,string part_number)
        {
            object obj;
            //try
            //{
                if(els.AddErrorLog(config_id, error_name, machine_name, responsible_id, work_order, part_number))
                {
                    obj = common.ResponseStr((int)httpStatus.succes, "调用成功");                 
                }
                else
                {
                    obj = common.ResponseStr((int)httpStatus.dbError, "添加失败");
                }
                
            //}
            //catch (Exception ex)
            //{
            //    obj = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            //}
            return Json(obj);
        }
        /// <summary>
        /// 异常签到或解除
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="type">0:签到  1:解除</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPut]
        public ActionResult<common.response> Put(int id,int type)
        {
            object obj;
            if (els.UpdataHandleTime(id, type))
            {
                obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
            }
            else
            {
                obj = common.ResponseStr((int)httpStatus.dbError, "修改失败");
            }
            return Json(obj);
        }
    }
}