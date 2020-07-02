using mpm_web_api.Common;
using mpm_web_api.db;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.DAL
{
    public class NoticeService: SqlSugarBase
    {
        public bool PostTextMessage(string agentid, string access_token, List<int> toparty, List<string> touserList, string text)
        {
            bool result = false;
            try
            {
                string url = "https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}";
                url = string.Format(url, access_token);
                string postData = ReplaceTextMessageJson(agentid, toparty, touserList, text);
                string information = HttpHelper.HttpPost(url, postData, null, null);
                int errcode = AnalysisTextMessageJson(information);
                if (errcode == 0)
                {
                    result = true;
                }

            }
            catch(Exception ex)
            {
                result = false;
            }
            return result;
        }




        public int AnalysisTextMessageJson(string jsonText)
        {
            try
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);
                int errcode = Convert.ToInt32(jo["errcode"]);
                return errcode;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public string GetAccessToken(string corpid, string corpsecret)
        {
            string access_token = string.Empty;
            try
            {
                string url = "https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}";

                url = string.Format(url, corpid, corpsecret);
                Console.WriteLine(url);
                string result = Common.HttpHelper.HttpGet(url);
                Console.WriteLine(result);
                if (!string.IsNullOrEmpty(result))
                {
                    access_token = AnalysisAccessTokenJson(result);
                }
            }
            catch
            {
                access_token = string.Empty;
            }
            return access_token;
        }

        public string AnalysisAccessTokenJson(string jsonText)
        {
            string access_token = string.Empty;
            try
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);
                int errcode = Convert.ToInt32(jo["errcode"]);
                if (errcode == 0)//出错返回码，为0表示成功，非0表示调用失败
                {
                    access_token = jo["access_token"].ToString();
                    int expires_in = Convert.ToInt32(jo["expires_in"]);

                }
            }
            catch
            {
                access_token = string.Empty;
            }
            return access_token;
        }

        public string ReplaceTextMessageJson(string agentid, List<int> toparty, List<string> touser, string text)
        {
            string jsonText = "{\"touser\" : \"\",\"toparty\" : \"\",\"totag\" : \"\",\"msgtype\" : \"text\",\"agentid\" : 0,\"text\" : {\"content\" : \"\"},\"safe\":0}";
            JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);
            jo["toparty"] = string.Join("|", toparty.ToArray());
            jo["touser"] = string.Join("|", touser.ToArray());
            jo["agentid"] = agentid;
            jo["text"]["content"] = text;
            return jo.ToString();
        }
    }
}
