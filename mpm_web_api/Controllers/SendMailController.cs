using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mpm_web_api.DAL;
using mpm_web_api.model;
using mpm_web_api.model.m_notice;
using Swashbuckle.AspNetCore.Annotations;

namespace mpm_web_api.Controllers
{
    [ApiExplorerSettings(GroupName = "Notice")]
    [Produces(("application/json"))]
    [Route("api/SendMail")]
    [SwaggerTag("发送邮件")]
    [ApiController]
    public class SendMailController : Controller
    {
        BaseService<email_server> emailService = new BaseService<email_server>();
        [HttpPost]
        public ActionResult<common.response> Post(sendmail_parameter sendmail_Parameter)
        {
            email_server email_info = emailService.QueryableToList().FirstOrDefault();
            string FromMial = email_info.user_name;
            List<string> ToMial_List = sendmail_Parameter.ToMialList;
            string AuthorizationCode = email_info.password;
            List<string> CCMial_List = sendmail_Parameter.CCMialList;
            string File_Path = sendmail_Parameter.File_Path;
            string Subject = sendmail_Parameter.Subject;
            string Body = sendmail_Parameter.Body;
            //实例化一个发送邮件类。
            MailMessage mailMessage = new MailMessage();

            //邮件的优先级，分为 Low, Normal, High，通常用 Normal即可
            mailMessage.Priority = MailPriority.Normal;

            //发件人邮箱地址。
            mailMessage.From = new MailAddress(FromMial);

            //收件人邮箱地址。需要群发就写多个
            //拆分邮箱地址
            for (int i = 0; i < ToMial_List.Count; i++)
            {
                mailMessage.To.Add(new MailAddress(ToMial_List[i]));  //收件人邮箱地址。
            }

            if (CCMial_List != null && CCMial_List.Count > 0)
            {
                List<string> CCMiallist = CCMial_List;
                for (int i = 0; i < CCMiallist.Count; i++)
                {
                    //邮件的抄送者，支持群发
                    //mailMessage.CC.Add(new MailAddress(CCMial));
                    mailMessage.CC.Add(new MailAddress(CCMial_List[i]));
                }
            }

            //邮件正文是否是HTML格式
            mailMessage.IsBodyHtml = false;

            //邮件标题。
            mailMessage.Subject = Subject;
            //邮件内容。
            mailMessage.Body = Body;

            //设置邮件的附件，将在客户端选择的附件先上传到服务器保存一个，然后加入到mail中  
            if (File_Path != "" && File_Path != null)
            {
                //将附件添加到邮件
                mailMessage.Attachments.Add(new Attachment(File_Path));
                //获取或设置此电子邮件的发送通知。
                mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            }

            //实例化一个SmtpClient类。
            SmtpClient client = new SmtpClient();

            #region 设置邮件服务器地址

            //在这里我使用的是163邮箱，所以是smtp.163.com，如果你使用的是qq邮箱，那么就是smtp.qq.com。
            // client.Host = "smtp.163.com";
            if (FromMial.Length != 0)
            {
                //根据发件人的邮件地址判断发件服务器地址   默认端口一般是25
                string[] addressor = FromMial.Trim().Split(new Char[] { '@', '.' });
                switch (addressor[1])
                {
                    case "163":
                        client.Host = "smtp.163.com";
                        break;
                    case "126":
                        client.Host = "smtp.126.com";
                        break;
                    case "qq":
                        client.Host = "smtp.qq.com";
                        break;
                    case "gmail":
                        client.Host = "smtp.gmail.com";
                        break;
                    case "advantech":
                        client.Host = "172.21.128.120";//outlook邮箱
                                                       //client.Port = 587;
                        break;
                    case "foxmail":
                        client.Host = "smtp.foxmail.com";
                        break;
                    case "sina":
                        client.Host = "smtp.sina.com.cn";
                        break;
                    default:
                        client.Host = "smtp.exmail.qq.com";//qq企业邮箱
                        break;
                }
            }
            #endregion

            //使用安全加密连接。
            client.EnableSsl = true;
            //不和请求一块发送。
            client.UseDefaultCredentials = false;

            //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
            client.Credentials = new NetworkCredential(FromMial, AuthorizationCode);

            //如果发送失败，SMTP 服务器将发送 失败邮件告诉我  
            mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            Console.WriteLine("next########");
            //发送
            client.Send(mailMessage);
            object obj = common.ResponseStr((int)httpStatus.succes, "调用成功");
            return Json(obj);
        }

    }
}
