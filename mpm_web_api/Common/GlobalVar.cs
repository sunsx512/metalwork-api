using mpm_web_api.model.m_common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.Common
{
    public static class GlobalVar
    {
        /// <summary>
        /// 是否为云端环境
        /// </summary>
        public static bool IsCloud = false;
        /// <summary>
        /// 授权数量
        /// </summary>
        public static int authorized_number = 0;
        /// <summary>
        /// 当前地区的时区
        /// </summary>
        public static double time_zone = 8;
        /// <summary>
        /// 当前使用的模块
        /// </summary>
        public static string module = "ALL";
        /// <summary>
        /// mqtt host
        /// </summary>
        public static string mqtthost = "172.21.168.23";
        /// <summary>
        /// mqtt 账号
        /// </summary>
        public static string mqttuser = "admin";
        /// <summary>
        /// mqtt 密码
        /// </summary>
        public static string mqttpwd = "public";
        /// <summary>
        /// mqtt 端口号
        /// </summary>
        public static int mqttport = 1883;
        /// <summary>
        /// 发送的topic
        /// </summary>
        public static string mqtttopic = "/iot-2/evt/wadata/fmt/andon";
        /// <summary>
        /// 登入权限认证的client信息 初始化时创建或获取
        /// </summary>
        public static client client;
        public enum Error_handle
        {
            trigger = 1,
            sign_in = 2,
            release = 3
        }
    }
}
