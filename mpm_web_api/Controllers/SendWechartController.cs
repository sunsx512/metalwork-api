using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.Common;
using mpm_web_api.DAL;
using mpm_web_api.model;
using mpm_web_api.model.m_notice;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers
{
    [ApiExplorerSettings(GroupName = "Notice")]
    [Produces(("application/json"))]
    [Route("api/SendWechart")]
    [SwaggerTag("发送微信")]
    [ApiController]
    public class SendWechartController : Controller
    {
        BaseService<wechart_server> wechartServer = new BaseService<wechart_server>();
        NoticeService noticeService = new NoticeService();
        [HttpPost]
        public ActionResult<common.response> Post(sendwechart_parameter sendwechart_parameter)
        {
            bool result = false;
            wechart_server wechartInfo = wechartServer.QueryableToList().FirstOrDefault();
            string corpid = wechartInfo.corp_id;
            string corpsecret = wechartInfo.apply_secret;
            string access_token = wechartInfo.access_token;
            string agentid = wechartInfo.apply_agentid;
            string applyname = wechartInfo.apply_name;
            int id = wechartInfo.id;
            DateTime? createtime = wechartInfo.create_time;
            //要加判空验证
            TimeSpan tSpan = (TimeSpan)(DateTime.Now.AddHours(GlobalVar.time_zone) - createtime);
            if (tSpan.TotalMinutes < 20)//判断access_token是否失效
            {
                result = noticeService.PostTextMessage(agentid, access_token, sendwechart_parameter.topartyLisit, sendwechart_parameter.touserList, sendwechart_parameter.text);
                if(result)
                {
                    object obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
                    return Json(obj);
                }
                else
                {
                    object obj = common.ResponseStr((int)httpStatus.clientError, "调用失败");
                    return Json(obj);
                }
            }
            else
            {
                string new_access_token = noticeService.GetAccessToken(corpid, corpsecret);//如果20分钟内，认为就快失效了，重新获取
                wechart_server update_wechart_server = new wechart_server();
                update_wechart_server.corp_id = corpid;
                update_wechart_server.apply_agentid = agentid;
                update_wechart_server.apply_secret = corpsecret;
                update_wechart_server.apply_name = applyname;
                update_wechart_server.id = id;
                update_wechart_server.access_token = new_access_token;//更新wechart_server
                update_wechart_server.create_time = DateTime.Now.AddHours(GlobalVar.time_zone);//更新wechart_server
                var postdata = JsonConvert.SerializeObject(update_wechart_server);
                bool re = wechartServer.Update(update_wechart_server,x=>x.id == 1);

                result = noticeService.PostTextMessage(agentid, new_access_token, sendwechart_parameter.topartyLisit, sendwechart_parameter.touserList, sendwechart_parameter.text);

                if (result)
                {
                    object obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
                    return Json(obj);
                }
                else
                {
                    object obj = common.ResponseStr((int)httpStatus.clientError, "调用失败");
                    return Json(obj);
                }

            }
        }

    }
}
