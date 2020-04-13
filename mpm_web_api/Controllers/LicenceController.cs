using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.DAL;
using mpm_web_api.model.m_common;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers
{
    [ApiExplorerSettings(GroupName = "Common")]
    [Produces(("application/json"))]
    [Route("api/v1/configuration/public/licence")]
    [SwaggerTag("授权设备数量")]
    [ApiController]
    public class LicenceController : Controller
    {

        private readonly IHostingEnvironment _hostingEnvironment;

        public LicenceController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        /// <summary>
        /// 获取已授权数量
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<Licence>> Get()
        {
            object obj;
            Licence_Original lco = LicenceHelper.ReadLicence();
            //验证Licence合法性
            if (LicenceHelper.CheckSpaceID(lco.unique_identifier))
            {
                Licence lc = new Licence();
                MachineService ms = new MachineService();
                //获取已使用的设备数量
                lc.used_number = ms.GetMachineCount();
                //获取已授权的设备数量
                lc.authorized_number = lco.machineNum;
                lc.expire_date = lco.expire_date;
                lc.module = lco.module;
                lc.version = lco.version;

                List<Licence> list = new List<Licence>();
                list.Add(lc);
                obj = common.ResponseStr((int)httpStatus.succes, "调用成功", list);
            }
            else
            {
                obj = common.ResponseStr(401, "未授权");
            }

            return Json(obj);
        }




        /// <summary>
        /// 更新授权数量
        /// </summary>
        /// <param name="licence_str">加密的授权字符串</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPost]
        public ActionResult<common.response> Post(Licence_Str licence_str)
        {
            object obj = null;
            try
            {          
                string lch = LicenceHelper.DESEncrypt(licence_str.Licence);
                if (lch != null)
                {
                    Licence_Original lco = JsonConvert.DeserializeObject<Licence_Original>(lch);
                    if (lco != null)
                    {
                        //如果之前没被授权的话 允许被授权
                        if (!LicenceHelper.CheckLicenceLog(licence_str.Licence))
                        {
                            //验证Licence是否合法
                            if (LicenceHelper.CheckSpaceID(lco.unique_identifier))
                            {
                                //覆盖原授权数
                                if (lco.type == 0)
                                {
                                    LicenceHelper.SaveLicence(lch);
                                    LicenceHelper.SaveLicenceLog(licence_str.Licence);
                                }
                                //新增授权数
                                else if (lco.type == 1)
                                {
                                    //获取之前的授权数量
                                    Licence_Original pre_licence = LicenceHelper.ReadLicence();
                                    //相加
                                    lco.machineNum = lco.machineNum + pre_licence.machineNum;
                                    LicenceHelper.SaveLicence(JsonConvert.SerializeObject(lco));
                                    LicenceHelper.SaveLicenceLog(licence_str.Licence);
                                }
                                obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
                                return Json(obj);
                            }
                            else
                            {
                                obj = common.ResponseStr(401, "非法授权");
                                return Json(obj);
                            }
                        }
                        else
                        {
                            obj = common.ResponseStr(401, "失效的授权");
                            return Json(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                obj = common.ResponseStr((int)httpStatus.serverError, ex.Message);
                
            }
            return Json(obj);
        }
    }
}