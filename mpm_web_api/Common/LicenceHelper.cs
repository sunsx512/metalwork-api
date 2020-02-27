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

        static   MachineService ms = new MachineService();
        public static Licence ReadLicence()
        {
            Licence lc = new Licence();
            Licence_Original lco = new Licence_Original();
            if (System.IO.File.Exists("Licence"))
            {
                string str = File.ReadAllText("Licence");
                lco = JsonConvert.DeserializeObject<Licence_Original>(str);   
                //获取已使用的设备数量
                lc.used_number = ms.GetMachineCount();
                //获取已授权的设备数量
                lc.authorized_number = lco.machineNum;
                return lc;
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
            IDictionary tp = Environment.GetEnvironmentVariables();
            string str = "";
            string Env_space_id = "";
            foreach (DictionaryEntry tt in tp)
            {
                if (tt.Key.ToString() == "VCAP_APPLICATION")
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
                    if (t.Key == "space_id")
                    {
                        Env_space_id = (string)t.Value;
                        break;
                    }
                }
            }
            if (Env_space_id == space_id)
                return true;
            else
                return false;
        }
    }
}
