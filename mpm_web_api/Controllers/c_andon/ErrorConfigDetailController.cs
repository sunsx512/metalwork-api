﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.DAL.andon;
using mpm_web_api.model;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_andon
{
    [ApiExplorerSettings(GroupName = "Andon")]
    [Produces(("application/json"))]
    [Route("/api/v1/configuration/andon/error_config_detail")]
    [SwaggerTag("异常配置")]
    [ApiController]
    public class ErrorConfigDetailController : Microsoft.AspNetCore.Mvc.Controller
    {
        ErrorConfigService ecs = new ErrorConfigService();
        /// <summary>
        /// 查询详细异常配置
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<error_config_detail>> Get()
        {
            object obj;
            //try
            //{
                List<error_config_detail> lty = ecs.QueryableToList();
                string strJson = JsonConvert.SerializeObject(lty);
                obj = common.ResponseStr<error_config_detail>((int)httpStatus.succes, "调用成功", lty);
            //}
            //catch (Exception ex)
            //{
            //    obj = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            //}

            return Json(obj);
        }
    }
}