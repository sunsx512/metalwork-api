using mpm_web_api.db;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
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
        /// 获取云端环境变量
        /// </summary>
        public static void GetEnv()
        {
            string str = "";
            string postgres = "postgresql";
            string mongo = "mongodb";
            IDictionary tp = Environment.GetEnvironmentVariables();
            foreach (DictionaryEntry tt in tp)
            {
                if (tt.Key.ToString() == "VCAP_SERVICES")
                {
                    str = tt.Value.ToString();
                    break;
                }
            }
            if (str != "")
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(str);
                foreach (var t in jo)
                {
                    if (t.Key.Contains("postgresql"))
                    {
                        postgres = t.Key;
                    }
                    if (t.Key.Contains("mongodb"))
                    {
                        mongo = t.Key;
                    }
                }
                //pg
                Console.WriteLine(jo[postgres][0]["credentials"]["host"].ToString());
                Console.WriteLine(jo[postgres][0]["credentials"]["port"].ToString());
                Console.WriteLine(jo[postgres][0]["credentials"]["username"].ToString());
                Console.WriteLine(jo[postgres][0]["credentials"]["database"].ToString());
                Console.WriteLine(jo[postgres][0]["credentials"]["password"].ToString());
                string pg = "Server={0};Port={1};Database={2};User Id={3};Password={4};";
                pg = string.Format(pg, jo[postgres][0]["credentials"]["host"].ToString(), jo[postgres][0]["credentials"]["port"].ToString(), jo[postgres][0]["credentials"]["database"].ToString(), jo[postgres][0]["credentials"]["username"].ToString(), jo[postgres][0]["credentials"]["password"].ToString());
                PostgreBase.connString = pg;
            }
        }
        /// <summary>
        /// 回复给网页的字符串
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="message">信息</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static string ResponseStr(int code,string message,string content)
        {
            string re = "\"code\":{0},\"message\": \"{1}\",\"data\": {2} ";
            if (code !=0 && message !="" && content !="")
            {
                re = string.Format(re, code, message, content);
                return "{"+re+"}";
            }
            else
            {
                return "";
            }
        }
    }
}
