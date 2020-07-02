using System;
using System.Collections.Generic;
using System.Text;
//using SqlSugar;

namespace mpm_web_api.model.m_notice
{
    public class sendwechart_parameter  
    {
        /// <summary>
        /// 部门id
        /// </summary>
        public List<int> topartyLisit { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>
        public List<string> touserList { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string text { get; set; }

        //public string returnMessage { get; set; }


}
}
