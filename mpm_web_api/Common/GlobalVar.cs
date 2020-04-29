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
        /// 数据库时区
        /// </summary>
        public static double db_time_zone = 0;
    }
}
