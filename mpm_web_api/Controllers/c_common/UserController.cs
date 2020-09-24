#define docker
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.DAL;
using mpm_web_api.model.m_common;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers.c_common
{
    [ApiExplorerSettings(GroupName = "Common")]
    [Produces(("application/json"))]
    [Route("api/v1/configuration/public/user")]
    [SwaggerTag("用户登录信息")]
    [ApiController]
    public class UserController : Microsoft.AspNetCore.Mvc.Controller
    {
        WisePaasUserService wpus = new WisePaasUserService();
        [HttpGet]
        public ActionResult<common.response> Get()
        {
            return Json(wpus.GetUser());
        }


        #if (docker)
        /// <summary>
        /// 新增或更新用户信息 权限分为三层  Admin Editor Viewer
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPost]
        public ActionResult<common.response> Post(wise_paas_user t)
        {
            object obj = common.ResponseStr((int)httpStatus.serverError, "调用失败");

            if(GlobalVar.IsCloud)
            {
                if (wpus.InsertInfo(t))
                {
                    obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
                }
                else
                {
                    obj = common.ResponseStr((int)httpStatus.serverError, "调用失败");
                }
            }
            else
            {
                //权限字符串卡关
                if (t.role == "Editor" || t.role == "Viewer")
                {
                    if (wpus.InsertInfo(t))
                    {
                        obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
                    }
                    else
                    {
                        obj = common.ResponseStr((int)httpStatus.serverError, "调用失败");
                    }
                }
            }
            return Json(obj);
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user">用户名</param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult<common.response> Delete(string user)
        {
            //如果是初始账号 则不可删除
            object obj = common.ResponseStr((int)httpStatus.serverError, "调用失败");
            if (user == "admin")
            {
                obj = common.ResponseStr((int)httpStatus.serverError, "调用失败");
                return Json(obj);
            }

            if (wpus.DeleteUser(user))
            {
                obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
            }
            else
            {
                obj = common.ResponseStr((int)httpStatus.serverError, "调用失败");
            }
            return Json(obj);
        }

        /// <summary>
        /// 验证登录权限
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<common.response> Patch(string user, string password)
        {
            return Json(wpus.Check(user, password));
        }
        #else
        /// <summary>
        /// 保存账号密码 供worker使用
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<common.response> Post(wise_paas_user t)
        {
            object obj = common.ResponseStr((int)httpStatus.serverError, "调用失败");
            //权限字符串卡关
            if (wpus.InsertInfo(t))
            {
                obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
            }
            else
            {
                obj = common.ResponseStr((int)httpStatus.serverError, "调用失败");
            }
            return Json(obj);
        }
        #endif
    }
}