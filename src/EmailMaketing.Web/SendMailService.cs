using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailMaketing.Web
{
    public class SendMailService : ISendEmailAppServices
    {
        public SendMailService()
        {

        }
        // private readonly MailSettings mailSettings;
        public async Task SendMail(MailContent emailParameter, string emailaddress, string name, string pass, List<string> listfile)
        {

            //string emailaddress = "Henrydao0810@gmail.com";
            //string name = "Tran Van Dao";
            string host = "smtp.gmail.com";
            int port = 587;
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(name, emailaddress);
            email.From.Add(new MailboxAddress(name, emailaddress));
            email.To.Add(MailboxAddress.Parse(emailParameter.To));
            email.Subject = emailParameter.Subject;
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                var builder = new BodyBuilder();
                if (listfile.Count > 0)
                {
                    foreach (var file in listfile)
                    {
                        builder.Attachments.Add(file);
                    }
                }
                builder.HtmlBody = emailParameter.Body;
                email.Body = builder.ToMessageBody();
                smtp.Connect(host, port, SecureSocketOptions.StartTls);
                smtp.Authenticate(emailaddress, pass);
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                System.IO.Directory.CreateDirectory("mailssave");
                var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                await email.WriteToAsync(emailsavefile);
            }

            smtp.Disconnect(true);

        }
        //leuzxdmiwryorxxi pass application
        public async Task SendEmailAsync(string email, string subject, string htmlMessage, string name, string emailaddress, string pass, List<string> listfile)
        {
            await SendMail(new MailContent()
            {
                To = email,
                Subject = subject,
                Body = htmlMessage
            }, emailaddress, name, pass, listfile);
        }
        public string checkemail(string email, string pass)
        {
            try
            {
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(email, pass);
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
