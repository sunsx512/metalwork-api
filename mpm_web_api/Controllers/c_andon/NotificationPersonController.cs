using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.DAL.andon;
using mpm_web_api.model;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers
{

    [Produces(("application/json"))]
    [Route("/api/v1/configuration/andon/notification_person")]
    [SwaggerTag("通知人员")]
    [ApiController]
    public class NotificationPersonController : SSOController
    {
        ControllerHelper<notification_person> ch = new ControllerHelper<notification_person>();
        [HttpDelete]
        public ActionResult<common.response> Delete(int id)
        {
            return Json(ch.Delete(id));
        }
        [HttpGet]
        public ActionResult<common.response<notification_person>> Get()
        {
            return Json(ch.Get());
        }
        [HttpPost]
        public ActionResult<common.response> Post(notification_person t)
        {
            return Json(ch.Post(t));
        }
        [HttpPut]
        public ActionResult<common.response> Put(notification_person t)
        {
            return Json(ch.Put(t));
        }
    }
}