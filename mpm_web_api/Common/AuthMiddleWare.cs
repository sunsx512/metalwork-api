﻿using Microsoft.AspNetCore.Http;
using mpm_web_api.DAL;
using mpm_web_api.model.m_common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wise_Paas;
using Wise_Paas.models;
using wise_paas_sso.models;
using Wise_Pass;

namespace mpm_web_api.Common
{
    public class AuthMiddleWare
    {
        private readonly RequestDelegate next;
        static ApiLogService aes = new ApiLogService();
        public AuthMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                string token = context.Request.Headers["Authorization"].ToString();
                //直接放行
                if(context.Request.Path.Value == "/api/v1/configuration/public/Client")
                {
                    await next(context);
                }
                else
                {
                    if (token != "")
                    {
                        if (token.Contains("Bearer"))
                        {
                            token = token.Split(' ')[1];
                            EnvironmentInfo environmentInfo = EnvironmentVariable.Get();
                            if (GlobalVar.client != null)
                            {
                                SrpRole srpRole = SSO.GetRole(GlobalVar.client.clientId, token, environmentInfo.sso_url);
                                //只有有权限的情况 才会放行
                                if (srpRole != null)
                                {
                                    await next(context);
                                }
                            }
                        }
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
                
            }
        }



    }
}
