using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_common
{
    public class Licence
    {
        /// <summary>
        /// 已使用的设备数
        /// </summary>
        public int used_number { set; get; }
        /// <summary>
        /// 授权数量
        /// </summary>
        public int authorized_number { set; get; }
        /// <summary>
        /// 过期日期
        /// </summary>
        public DateTime expire_date { set; get; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string version { set; get; }
        /// <summary>
        /// 功能
        /// </summary>
        public string module { set; get; }
    }
    public class Licence_Original
    {
        /// <summary>
        /// 唯一性标识
        /// </summary>
        public string unique_identifier { set; get; }
        /// <summary>
        /// 授权数量
        /// </summary>
        public int machineNum { set; get; }
        /// <summary>
        /// 授权类型 0:覆盖数量 1:新增数量
        /// </summary>
        public int type { set; get; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime expire_date { set; get; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string version { set; get; }
        /// <summary>
        /// 功能
        /// </summary>
        public string module { set; get; }
    }

    public class Licence_Str
    {
        /// <summary>
        /// 加密的授权码
        /// </summary>
        public string Licence { set; get; }

    }
}
