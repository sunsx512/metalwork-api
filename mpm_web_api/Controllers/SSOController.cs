using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace mpm_web_api.Controllers
{
    public class SSOController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //string token = filterContext.HttpContext.Request.Headers["Authorization"];
            //string ssourl = "https://" + "portal-sso.wise-paas.cn/v2.0/tokenvalidation";
            //string reurl = "https://localhost:5001/error";
            //if(token != null)
            //{
            //    token = token.Replace("Bearer ", "");
            //    token = "{" + "\"token\"" + ":\"" + token + "\"" + "}";
            //    string str = HttpPost(ssourl, token);
            //    if(str == null)
            //    {
            //        filterContext.Result = new RedirectResult(reurl);
            //    }
            //}
            //else
            //{
            //    filterContext.Result = new RedirectResult(reurl);
            //}
            base.OnActionExecuting(filterContext);
            return ;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return base.OnActionExecutionAsync(context, next);
        }


        public static string HttpPost(string url, string body)
        {
            try
            {
                Encoding encoding = Encoding.UTF8;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Timeout = 5000;
                request.Accept = "application/json";
                request.ContentType = "application/json";
                byte[] buffer = encoding.GetBytes(body);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
    }
}