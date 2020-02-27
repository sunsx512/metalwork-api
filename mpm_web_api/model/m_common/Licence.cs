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
    }
    public class Licence_Original
    {
        /// <summary>
        /// 空间id
        /// </summary>
        public string space_id { set; get; }
        /// <summary>
        /// 授权数量
        /// </summary>
        public int machineNum { set; get; }
        /// <summary>
        /// 授权类型 0:覆盖数量 1:新增数量
        /// </summary>
        public int type { set; get; }
    }

    public class Licence_Str
    {
        /// <summary>
        /// 加密的授权码
        /// </summary>
        public string Licence { set; get; }

    }
}
