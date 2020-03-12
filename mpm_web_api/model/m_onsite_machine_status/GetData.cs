using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OnsiteStatusWorker.Models
{
    public class GetData
    {
        public string GetDatas() {
            string data = "";
            string licencePostUrl = "url" + "api/v1/configuration/public/licence";
            string licencePostData = "{{" +
                "\"licence\":\"{0}\"" +
                "}}";

            licencePostData = string.Format(licencePostData, data);
            string licencePostResult = GetUrl(licencePostUrl);
            JObject joLicencePost = (JObject)JsonConvert.DeserializeObject(licencePostResult);
            if (Convert.ToInt32(joLicencePost["code"]) == 200)
            {
                return "Success";
            }
            else
            {
                return "Error";
            }
        }


        /// <summary>
        /// http get方法
        /// </summary>
        /// <param name="Url">url地址</param>
        /// <param name="paramName">参数名称</param>
        /// <param name="postDataStr">抛送参数</param>
        /// <returns></returns>
        public string GetUrl(string Url)
        {
            string result = null;
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(Url);
                req.Method = "GET";
                req.ContentType = "text/html;charset=UTF-8";
                req.Timeout = 80000;
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream myResponseStream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                result = reader.ReadToEnd();
                reader.Close();
                myResponseStream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;

        }
    }
}
