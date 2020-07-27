using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_common
{
    public class area_property_break
    {
        public List<rest> rest { set; get; }
    }

    public class rest
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public string start { set; get; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string end { set; get; }
    }
}
