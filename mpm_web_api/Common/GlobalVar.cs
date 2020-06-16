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

        public enum Error_handle
        {
            trigger = 1,
            sign_in = 2,
            release = 3
        }
    }
}
