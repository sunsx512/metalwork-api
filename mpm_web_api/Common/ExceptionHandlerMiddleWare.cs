﻿using Microsoft.AspNetCore.Http;
using mpm_web_api.DAL;
using mpm_web_api.model.m_common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.Common
{
    public class ExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate next;
        static ApiExceptionLogService aes = new ApiExceptionLogService();
        public ExceptionHandlerMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
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

            await WriteExceptionAsync(context, exception).ConfigureAwait(false);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception exception)
        {
            api_exception_log ael = new api_exception_log();
            ael.path = context.Request.Path;
            ael.content = exception.Message;
            ael.insert_time = DateTime.Now;
            ael.method = context.Request.Method;
            //插入日志
            aes.InsertLog(ael);
            //返回友好的提示
            HttpResponse response = context.Response;
            response.ContentType = context.Request.Headers["Accept"];

            response.ContentType = "application/json";
            await response.WriteAsync(JsonConvert.SerializeObject(common.ResponseStr((int)httpStatus.serverError, exception.Message))).ConfigureAwait(false);
        }
    }
}