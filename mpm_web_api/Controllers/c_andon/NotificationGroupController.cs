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

namespace mpm_web_api.Controllers.c_andon
{
    [Produces(("application/json"))]
    [Route("/api/v1/configuration/andon/notification_group")]
    [SwaggerTag("通知人员群组")]
    [ApiController]
    public class NotificationGroupController : SSOController
    {
        ControllerHelper<notification_group> ch = new ControllerHelper<notification_group>();
        ControllerHelper<notification_person> chp = new ControllerHelper<notification_person>();
        NotificationGroupService ngs = new NotificationGroupService();
        [HttpDelete]
        public ActionResult<common.response> Delete(int id)
        {
            return Json(ch.Delete(id));
        }

        /// <summary>
        /// 删除该群组下的指定人员
        /// </summary>
        /// <param name="group_id">群组名字</param>
        /// <param name="person_id">人员id</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpDelete("{group_id}")]
        public ActionResult<common.response> Delete(int group_id, int person_id)
        {
            object obj;
            try
            {
                if (ngs.DeletePerson(group_id, person_id))
                {
                    obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
                }
                else
                {
                    obj = common.ResponseStr((int)httpStatus.dbError, "删除失败");
                }

            }
            catch (Exception ex)
            {
                obj = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            }
            return Json(obj);
        }

        /// <summary>
        /// 获取详细的通知群组信息
        /// </summary>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpGet]
        public ActionResult<common.response<notification_group_detail>> Get()
        {
            object obj;
            try
            {
                List<notification_group_detail> lty = ngs.QueryableToList();
                string strJson = JsonConvert.SerializeObject(lty);
                obj = common.ResponseStr<notification_group_detail>((int)httpStatus.succes, "调用成功", lty);
            }
            catch (Exception ex)
            {
                obj = common.ResponseStr((int)httpStatus.serverError, ex.Message);
            }

            return Json(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPost]
        public ActionResult<common.response> Post(notification_group t)
        {
            return Json(ch.Post(t));
        }

        /// <summary>
        /// 添加该群组下的人员
        /// </summary>
        /// <param name="group_id">群组id</param>
        /// <param name="person_id">人员id</param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPost("{group_id}")]
        public ActionResult<common.response> Post(int group_id,int person_id)
        {
            notification_person np = new notification_person();
            np.notification_group_id = group_id;
            np.person_id = person_id;
            return Json(chp.Post(np));
        }

        /// <summary>
        /// 更新通知群组信息
        /// </summary>
        /// <param name="t"></param>
        /// <response code="200">调用成功</response>
        /// <response code="400">服务器异常</response>
        /// <response code="410">数据库操作失败</response>
        /// <response code="411">外键异常</response>
        [HttpPut]
        public ActionResult<common.response> Put(notification_group t)
        {
            return Json(ch.Put(t));
        }
    }
}