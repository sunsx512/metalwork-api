using System;
using System.Collections.Generic;
using System.Text;
//using SqlSugar;

namespace mpm_web_api.model.m_notice
{
    public class sendmail_parameter  
    {
        /// <summary>
        /// 收件人
        /// </summary>
        public List<string> ToMialList { get; set; }
        /// <summary>
        /// 抄送人
        /// </summary>
        public List<string> CCMialList { get; set; }
        /// <summary>
        /// 附件路径
        /// </summary>
        public string File_Path { get; set; }
        /// <summary>
        /// 邮件主题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Body { get; set; }
        
    }
}
