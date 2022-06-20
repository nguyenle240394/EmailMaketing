using EmailMaketing.ContentEmails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MailKit;
using EmailMaketing.Mails;
using MimeKit;
using System;
using MailKit.Security;
using System.Net.Mail;
using System.Net;
using EmailMaketing.Jobs;
using Abp.BackgroundJobs;
using System.Collections.Generic;

namespace EmailMaketing.Web.Pages.ContentEmails
{
    public class FormContentEmailModel : PageModel
    {
        private readonly ContentEmailAppService _ContentEmailAppService;
        private readonly RegistrationMailService _RegistrationMailService;
        public List<ContentEmailDto> ContentEmail { get; set; }
        public ContentEmailDto SelectEmail { get; set; }
        public FormContentEmailModel(ContentEmailAppService contentEmailAppService, RegistrationMailService registrationMailService)
        {
            _ContentEmailAppService = contentEmailAppService;
            _RegistrationMailService = registrationMailService;
        }

        public async Task OnGetAsync()
        {
            Guid g = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00");
            ContentEmail = await _ContentEmailAppService.GetListEmailAsync(g);
            foreach(var i in ContentEmail)
            {
               // i.id;
            }
           // id.Add(ContentEmail)
        }

        public async Task<IActionResult> OnPostSendPreview(string nameEmail)
        {
            //SendMailService sm = new SendMailService();
            //await sm.SendEmailAsync("tmson.it@gmail.com", "This is email", "hello word");
            await _RegistrationMailService.RegisterAsync("asdfsfasdf");
            return new JsonResult("OK");
        }
        public async Task<IActionResult> OnPostAddEmail(string id)
        {
            Guid g = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00");
            string name = Request.Form["subject"];
            if (name == "" || name == null) name = "No Subject";
            var Data = new CreateUpdateContentEmailDto();
            Data.Subject = name;
            Data.Time = System.DateTime.Now;
            Data.CustomerID = g;
            await _ContentEmailAppService.CreateAsync(Data);
            ContentEmail = await _ContentEmailAppService.GetListEmailAsync(g);
            return new JsonResult(ContentEmail.Count);
        }
        public async Task<IActionResult> OnPostSelectEmail(string id)
        {
            Guid guid = new Guid(id);
            SelectEmail = await _ContentEmailAppService.GetEmailAsync(guid);
            return new JsonResult(SelectEmail);
        }
        public async Task<IActionResult> OnPostDeleteEmail(string id)
        {
            Guid guid = new Guid(id);
            var check = await _ContentEmailAppService.DeleteAsync(guid);
            if(check == true)
            {
                return new JsonResult("OK");
            }
            else
            {
                return new JsonResult("Error");
            }
        }

        public void OnPostUpdateEmail(string str_json)
        {

        }
    }
    //public class SendMailService : ISendMailService
    //{
    //   // private readonly MailSettings mailSettings;
    //    public async Task SendMail(EmailParameter emailParameter)
    //    {
    //        string emailaddress = "Henrydao0810@gmail.com";
    //        string name = "Tran Van Dao";
    //        string pass = "leuzxdmiwryorxxi";
    //        string host = "smtp.gmail.com"; //smtp.ethereal.email
    //        int port = 587;
    //        var email = new MimeMessage();
    //        email.Sender = new MailboxAddress(name, emailaddress);
    //        email.From.Add(new MailboxAddress(name, emailaddress));
    //        email.To.Add(MailboxAddress.Parse(emailParameter.To));
    //        email.Subject = emailParameter.Subject;


    //        var builder = new BodyBuilder();
    //        builder.HtmlBody = emailParameter.Body;
    //        email.Body = builder.ToMessageBody();

    //        using var smtp = new MailKit.Net.Smtp.SmtpClient();

    //        try
    //        {
    //            smtp.Connect(host, port, SecureSocketOptions.StartTls);
    //            smtp.Authenticate(emailaddress, pass);
    //            await smtp.SendAsync(email);
    //        }
    //        catch (Exception ex)
    //        {
    //            System.IO.Directory.CreateDirectory("mailssave");
    //            var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
    //            await email.WriteToAsync(emailsavefile);
    //        }

    //        smtp.Disconnect(true);

    //    }
    //    //leuzxdmiwryorxxi pass application
    //    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    //    {
    //        await SendMail(new EmailParameter()
    //        {
    //            To = email,
    //            Subject = subject,
    //            Body = htmlMessage
    //        });
    //    }
    //}
}
