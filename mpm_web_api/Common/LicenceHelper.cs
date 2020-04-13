using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Hosting;
using mpm_web_api.DAL;
using mpm_web_api.model.m_common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Wise_Paas.models;
using Wise_Pass;

namespace mpm_web_api.Common
{
    public static class LicenceHelper
    {

        public static string key = "WisePaaS";
        public static string iv = "AKTCKM30";

        public static bool SaveLicence(string str)
        {
            if (System.IO.File.Exists("Licence"))
            {
                System.IO.File.Delete("Licence");
            }

            File.Create("Licence").Dispose();
            File.WriteAllText("Licence", str);
            return true;
        }

        /// <summary>
        /// 保存已授权许可证(防止多次授权使用)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool SaveLicenceLog(string str)
        {
            if (!System.IO.File.Exists("LicenceLog"))
            {
                File.Create("LicenceLog").Dispose();
            }      
            File.AppendAllText("LicenceLog", str+ "\r\n");
            return true;
        }

        public static List<string> ReadLicenceLog()
        {
            List<string> list = new List<string>();
            if (System.IO.File.Exists("LicenceLog"))
            {
                list = File.ReadAllLines("LicenceLog").ToList();
            }
            
            return list;
        }

        /// <summary>
        /// 检查Licence是否已经被授权了
        /// </summary>
        /// <param name="str">外部Licence字符串</param>
        /// <returns></returns>
        public static bool CheckLicenceLog(string str)
        {
            List<string> list = ReadLicenceLog();
            if( list != null)
            {
                foreach(string lc in list)
                {
                    if (lc == str)
                        return true;
                }
            }
            return false;
        }

        public static Licence_Original ReadLicence()
        {
            Licence_Original lco = new Licence_Original();
            if (System.IO.File.Exists("Licence"))
            {
                string str = File.ReadAllText("Licence");
                lco = JsonConvert.DeserializeObject<Licence_Original>(str);

                return lco;
            }
            else
            {
                return null;
            }
        }

        //解密
        public static string DESEncrypt(string data)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(key);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(iv);
            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(data);
            }
            catch
            {
                return null;
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }

        /// <summary>
        /// 验证spaceID是否正确
        /// </summary>
        /// <returns></returns>
        public static bool CheckSpaceID(string space_id)
        {
            string Env_space_id = "";
            EnvironmentInfo environmentInfo = EnvironmentVariable.Get();

            //EnSaaS 4.0 环境
            if (environmentInfo.cluster != null)
            {
                Env_space_id = environmentInfo.workspace;
                if (Env_space_id == space_id)
                    return true;
                else
                    return false;
            }
            //docker 环境
            else
            {
                string mac_path = Environment.GetEnvironmentVariable("MAC_PATH");
                string mac_list = File.ReadAllText(mac_path);
                //只要符合一个mac地址就可以授权
                if(mac_list != null)
                {
                    string[] str_list = mac_list.Split("\r\n");
                    foreach(string str in str_list)
                    {
                        Env_space_id = str;
                        if (Env_space_id == space_id)
                            return true;
                    }
                }
            }
            return false;

        }
    }
}
