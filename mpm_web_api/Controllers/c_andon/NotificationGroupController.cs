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
        NotificationGroupService ngs = new NotificationGroupService();
        [HttpDelete]
        public ActionResult<common.response> Delete(int id)
        {
            return Json(ch.Delete(id));
        }
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
                obj = common.ResponseStr<notification_group_detail>((int)httpStatus.serverError, ex.Message);
            }

            return Json(obj);
        }
        [HttpPost]
        public ActionResult<common.response> Post(notification_group t)
        {
            return Json(ch.Post(t));
        }
        [HttpPut]
        public ActionResult<common.response> Put(notification_group t)
        {
            return Json(ch.Put(t));
        }
    }
}