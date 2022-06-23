using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EmailMaketing.ContentEmails
{
    public class ContentEmailAppService : ApplicationService, IContentEmailAppService
    {
        private readonly IContentEmailRepository _ContentEmailRepository;
        public ContentEmailAppService(IContentEmailRepository contentEmailRepository)
        {
            _ContentEmailRepository = contentEmailRepository;
        }

        public string CheckEmail(string Address, string pass)
        {
            try
            {
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(Address, pass);
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<ContentEmailDto> CreateAsync(CreateUpdateContentEmailDto input)
        {
            var contentEmail = ObjectMapper.Map<CreateUpdateContentEmailDto, ContentEmail>(input);
            await _ContentEmailRepository.InsertAsync(contentEmail);
            return ObjectMapper.Map<ContentEmail,ContentEmailDto>(contentEmail);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var findid = await _ContentEmailRepository.FindAsync(id);
            if (findid == null) return false;
            else
            {
                await _ContentEmailRepository.DeleteAsync(id);
                return true;
            }
        }

        public async Task<ContentEmailDto> GetEmailAsync(Guid id)
        {
            var GetaEmail = await _ContentEmailRepository.GetAsync(id);
            return ObjectMapper.Map<ContentEmail, ContentEmailDto>(GetaEmail);
        }

        public async Task<List<ContentEmailDto>> GetListsEmailAsync(Guid id)
        {
            var ContentEmails = await _ContentEmailRepository.GetListAsync();
            var listContentEmails = ContentEmails.Where(x => x.CustomerID == id).ToList();
            var ContentEmaiDtos = ObjectMapper.Map<List<ContentEmail>, List<ContentEmailDto>>(listContentEmails);
            return ContentEmaiDtos;
        }

        public async Task SendMailAsync(string to, string subject, string body, string emailaddress, string name, string pass, List<string> listfile)
        {
            //string emailaddress = "Henrydao0810@gmail.com";
            //string name = "Tran Van Dao";
            string host = "smtp.gmail.com";
            int port = 587;
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(name, emailaddress);
            email.From.Add(new MailboxAddress(name, emailaddress));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
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
                builder.HtmlBody = body;
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

        public async Task<ContentEmailDto> UpdateDataAsync(Guid id, CreateUpdateContentEmailDto input)
        {
            var ContentEmails = await _ContentEmailRepository.FindAsync(id);
            ContentEmails.Subject = input.Subject;
            ContentEmails.Body = input.Body;
            ContentEmails.SenderEmail = input.SenderEmail;
            ContentEmails.Status = input.Status;
            ContentEmails.Attachment = input.Attachment;
            await _ContentEmailRepository.UpdateAsync(ContentEmails);
            return ObjectMapper.Map<ContentEmail, ContentEmailDto>(ContentEmails);
        }
    }
}
