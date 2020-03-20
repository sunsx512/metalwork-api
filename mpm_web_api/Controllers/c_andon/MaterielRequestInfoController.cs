using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.DAL.andon;
using mpm_web_api.model;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.Controllers.c_andon
{
    [ApiExplorerSettings(GroupName = "Andon")]
    [Produces(("application/json"))]
    [Route("/api/v1/configuration/andon/material_request_info")]
    [SwaggerTag("物料请求信息")]
    [ApiController]
    public class MaterielRequestInfoController:SSOController
    {
        MaterielRequestInfoService mris = new MaterielRequestInfoService();
        ControllerHelper<material_request_info> ch = new ControllerHelper<material_request_info>();
        /// <summary>
        /// 删除
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
        /// 查询异常类型
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<material_request_info>> Get()
        {
            return Json(ch.Get());
        }

        /// <summary>
        /// 查询异常类型
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet("{type}")]
        public ActionResult<common.response<material_request_info>> Get(int type,string machine_name)
        {
            List<material_request_info> list =  mris.QueryableToListByMachineAndStatus(type, machine_name);
            object obj = common.ResponseStr<material_request_info>((int)httpStatus.succes, "调用成功", list);
            return Json(obj);
        }


        /// <summary>
        /// 新增异常类型
        /// </summary>
        /// <param name="t"></param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPost]
        public ActionResult<common.response> Post(material_request_info t)
        {
            return Json(ch.Post(t));
        }

        /// <summary>
        /// 更新异常类型
        /// </summary>
        /// <param name="t"></param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPut]
        public ActionResult<common.response> Put(material_request_info t)
        {
            return Json(ch.Put(t));
        }
    }
}
