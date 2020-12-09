using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.DAL;
using mpm_web_api.model;
using mpm_web_api.model.m_common;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers
{
    [ApiExplorerSettings(GroupName = "Common")]
    [Produces(("application/json"))]
    [Route("api/v1/configuration/public/machine")]
    [SwaggerTag("设备")]
    [ApiController]
    public class MachineController : Controller,IController<machine>
    {
        ControllerHelper<machine> ch = new ControllerHelper<machine>();
        MachineService ms = new MachineService();
        /// <summary>
        /// 删除设备
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
        /// <summary>
        /// 获取所有设备信息
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<machine>> Get()
        {
            return Json(ch.Get());
        }
        ///// <summary>
        ///// 分页查询
        ///// </summary>
        ///// <param name="pageNum">页面</param>
        ///// <param name="pageSize">每页页数</param>
        ///// <returns></returns>
        //[HttpGet("separate")]
        //public ActionResult<common.response<machine>> GetS(int pageNum, int pageSize)
        //{
        //    List<machine> list = ms.GetMachines(pageNum, pageSize);
        //    object obj = common.ResponseStr<machine>(200, "调用成功", list);
        //    return Json(obj);
        //}
        /// <summary>
        /// 模糊查询设备
        /// </summary>
        /// <param name="name">设备名(中文)</param>
        /// <param name="pageNum">页面</param>
        /// <param name="pageSize">每页页数</param>
        /// <returns></returns>
        [HttpGet("separate")]
        public ActionResult<common.responsewithcount<machine>> GetSN(string name,int pageNum, int pageSize)
        {
            List<machine> list = null;
            int count = 0;
            if (name == null)
            {
                list = ms.GetMachines(pageNum, pageSize,ref count);
            }
            else
            {
                list = ms.GetMachinesByName(name, pageNum, pageSize,ref count);
            }
            object obj = common.ResponseStr<machine>(200, "调用成功", count, list);
            return Json(obj);
        }
        /// <summary>
        /// 新增设备
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPost]
        public ActionResult<common.response> Post(machine t)
        {
            //获取已使用的设备数量
            int used_number = ms.GetMachineCount();
            if(used_number >= GlobalVar.authorized_number - 1)
            {
                object obj = common.ResponseStr(401, "超出授权数");
                return Json(obj);
            }
            else
            {
                return Json(ch.Post(t));
            }       
        }
        /// <summary>
        /// 更新设备
        /// </summary>
        /// <param name="t">传入参数</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPut]
        public ActionResult<common.response> Put(machine t)
        {
            return Json(ch.Put(t));
        }
    }
}