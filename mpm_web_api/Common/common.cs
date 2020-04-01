﻿using mpm_web_api.db;
using mpm_web_api.DB;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api
{
    enum httpStatus{
        succes=200,
        dbError = 410,
        serverError = 400,
        clientError = 500,
    }
    public static class common
    {

        /// <summary>
        /// 回复给网页的字符串
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="message">信息</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static object ResponseStr<T>(int code, string message, List<T> content) where T : class, new()
        {
            response<T> re = new response<T>(content);
            re.code = code;
            re.message = message;
            return re;
        }

        public static object ResponseStr(int code, string message) 
        {
            response re = new response();
            re.code = code;
            re.message = message;
            return re;
        }

        public class response<T> where T : class, new()
        {
            public response(List<T> obj)
            {
                this.data = obj;
            }
            public int code { get; set; } 
            public string message { get; set; } 
            public List<T> data { get; set; }
        }
        public class response
        {
            public int code { get; set; }
            public string message { get; set; }
        }

    }
}
