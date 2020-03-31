using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wise_Paas;
using Wise_Paas.models;
using Wise_Pass;

namespace mpm_web_api.Common
{
    public class AuthMiddleWare
    {
        private readonly RequestDelegate next;
        static string client_id = "";
        public AuthMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {

                string token = "";
                //if (context.Request.Headers)
                //{

                //}
                EnvironmentInfo environmentInfo = EnvironmentVariable.Get();
                if (client_id == "")
                {
                    Client client = SSO.GetClient("IFactory-Metalwork", token, environmentInfo.sso_url);
                    if(client != null)
                    {
                        client_id = client.clientId;
                    }
                }
                if (client_id != "")
                {
                    //只有有权限的情况 才会放行
                   if(Integration.CheckRole(context, "IFactory-Metalwork"))
                    {
                        await next(context);
                    }
                }
                //没有权限的话返回401
                HttpResponse response = context.Response;
                response.ContentType = context.Request.Headers["Accept"];
                response.ContentType = "application/json";
                await response.WriteAsync(JsonConvert.SerializeObject(common.ResponseStr(401, "No Authority"))).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception == null)
            {

                return;
            }
        }
    }
}
