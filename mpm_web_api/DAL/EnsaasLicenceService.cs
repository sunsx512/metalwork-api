using Microsoft.Extensions.Hosting;
using mpm_web_api.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wise_Paas.models;
using Wise_Pass;

namespace mpm_web_api.DAL
{
    public class EnsaasLicenceService : BackgroundService
    {
        //string baseurl = "https://api-license-ensaas.hz.wise-paas.com.cn/";
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string pn = Environment.GetEnvironmentVariable("PN");
            string licence_server_url = Environment.GetEnvironmentVariable("license_server_url");
            EnvironmentInfo environmentInfo = EnvironmentVariable.Get();
            
            while (!stoppingToken.IsCancellationRequested)
            {
                await new TaskFactory().StartNew(() =>
                {
                    try
                    {
                        string service_id = environmentInfo.cluster + environmentInfo.workspace + environmentInfo.@namespace;
                        string url = licence_server_url + "/api/partNum/licenseQty?pn={0}&id={1}";
                        url = string.Format(url, pn, service_id);
                        string str = Get(url);
                        Licence licence = JsonConvert.DeserializeObject<Licence>(str);
                        if (licence != null)
                        {
                            if (licence.isValidTransaction == true)
                            {
                                //认证通过 
                                GlobalVar.authorized_number = Convert.ToInt32(licence.number)*10;

                            }
                        }

                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp);
                        //错误处理
                    }

                    //定时任务休眠
                    Thread.Sleep(10 * 1000);
                });
            }

        }

        public class Licence
        {
            public string id { get; set; }
            public string pn { get; set; }
            public string subscriptionId { get; set; }
            public string datacenterCode { get; set; }
            public bool isValidTransaction { get; set; }
            public string number { get; set; }
            public string authcode { get; set; }
            public string activeInfo { get; set; }

        }

        public string Get(string Url)
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
