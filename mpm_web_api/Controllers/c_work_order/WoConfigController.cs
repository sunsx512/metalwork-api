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
    [Produces(("application/json"))]
    [Route("api/v1/configuration/work_order/wo_config")]
    [SwaggerTag("设定工单信息")]
    [ApiController]
    public class WoConfigController : SSOController
    {
        ControllerHelper<wo_config> ch = new ControllerHelper<wo_config>();
        WoConfigService wcs = new WoConfigService();

        /// <summary>
        /// 删除工单信息
        /// </summary>
        /// <param name="id"></param>
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
        /// 获取工单详细信息
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<wo_config_detail>> Get()
        {
            object obj;
            try
            {
                List<wo_config_detail> lty = wcs.QueryableToList();
                string strJson = JsonConvert.SerializeObject(lty);
                obj = common.ResponseStr<wo_config_detail>((int)httpStatus.succes, "调用成功", lty);
            }
            catch (Exception ex)
            {
                obj = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            }

            return Json(obj);
        }



        /// <summary>
        /// 获得该设备可开始的工单
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet("{machine_id}")]
        public ActionResult<common.response<wo_config>> Get(int machine_id)
        {
            object obj;
            try
            {
                List<wo_config> lty = wcs.QueryableByMachine(machine_id);
                string strJson = JsonConvert.SerializeObject(lty);
                obj = common.ResponseStr<wo_config>((int)httpStatus.succes, "调用成功", lty);
            }
            catch (Exception ex)
            {
                obj = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            }
            return Json(obj);
        }

        /// <summary>
        /// 新增工单信息
        /// </summary>
        /// <param name="t"></param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPost]
        public ActionResult<common.response> Post(wo_config t)
        {
            return Json(ch.Post(t));
        }

        /// <summary>
        /// 更新工单信息
        /// </summary>
        /// <param name="t"></param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPut]
        public ActionResult<common.response> Put(wo_config t)
        {
            return Json(ch.Put(t));
        }
    }
}