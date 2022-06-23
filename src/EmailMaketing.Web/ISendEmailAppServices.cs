using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailMaketing.Web
{
    public interface ISendEmailAppServices
    {
        Task SendMail(MailContent mailcontent, string emailaddress, string name, string pass, List<string> listfile);
        Task SendEmailAsync(string email, string subject, string htmlMessage, string name, string emailaddress, string pass, List<string> listfile);
    }
    public class MailContent
    {
        public string To { get; set; }              // Địa chỉ gửi đến
        public string Subject { get; set; }         // Chủ đề (tiêu đề email)
        public string Body { get; set; }            // Nội dung (hỗ trợ HTML) của email

    }
}
