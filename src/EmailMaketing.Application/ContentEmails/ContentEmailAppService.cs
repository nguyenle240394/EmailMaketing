using EmailMaketing.Customers;
using EmailMaketing.SenderEmails;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;

namespace EmailMaketing.ContentEmails
{
    public class ContentEmailAppService : ApplicationService, IContentEmailAppService
    {
        private readonly IContentEmailRepository _ContentEmailRepository;
        private readonly ISenderEmailRepository _senderEmailRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICurrentUser _currentUser;

        public ContentEmailAppService(IContentEmailRepository contentEmailRepository, 
            ISenderEmailRepository senderEmailRepository, ICustomerRepository customerRepository,
            ICurrentUser currentUser)        
        {
            _ContentEmailRepository = contentEmailRepository;
            _senderEmailRepository = senderEmailRepository;
            _customerRepository = customerRepository;
            _currentUser = currentUser;
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
        public int coutEmailSended = 0;
        public async Task<int> SendMailAsync(string to, string subject, string body, string emailaddress, string name, string pass, List<string> listfile)
        {
            //string emailaddress = "Henrydao0810@gmail.com";
            //string name = "Tran Van Dao";
            string host = "smtp.gmail.com";
            int port = 587;
            var email = new MimeMessage();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            try
            { 
            email.Sender = new MailboxAddress(name, emailaddress);
            email.From.Add(new MailboxAddress(name, emailaddress));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
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
                coutEmailSended++;
            }
            catch (Exception ex)
            {
                System.IO.Directory.CreateDirectory("mailssave");
                var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                await email.WriteToAsync(emailsavefile);
            }

            smtp.Disconnect(true);
            return coutEmailSended;
        }

        public async Task<ContentEmailDto> UpdateDataAsync(Guid id, CreateUpdateContentEmailDto input)
        {
            var ContentEmails = await _ContentEmailRepository.FindAsync(id);
            ContentEmails.Subject = input.Subject;
            ContentEmails.Body = input.Body;
            /*ContentEmails.SenderEmail = input.SenderEmail;*/
            ContentEmails.Status = input.Status;
            ContentEmails.Attachment = input.Attachment;
            await _ContentEmailRepository.UpdateAsync(ContentEmails);
            return ObjectMapper.Map<ContentEmail, ContentEmailDto>(ContentEmails);
        }

        public string CheckEmailExist(string addressEmail)
        {
            if (EmailIsValid(addressEmail))
            {
                TcpClient tClient = new TcpClient("gmail-smtp-in.l.google.com", 25);
                string CRLF = "\r\n";
                byte[] dataBuffer;
                string ResponseString;
                NetworkStream netStream = tClient.GetStream();
                StreamReader reader = new StreamReader(netStream);
                ResponseString = reader.ReadLine();

                /* Perform HELO  to SMTP Server and get Response */
                dataBuffer = Encoding.ASCII.GetBytes("HELO KirtanHere" + CRLF);
                netStream.Write(dataBuffer, 0, dataBuffer.Length);
                ResponseString = reader.ReadLine();
                dataBuffer = Encoding.ASCII.GetBytes("MAIL FROM:<henrydao0810@gmail.com>" + CRLF);
                netStream.Write(dataBuffer, 0, dataBuffer.Length);
                ResponseString = reader.ReadLine();

                /* Read Response of the RCPT TO Message to know from google if it exist or not */
                dataBuffer = Encoding.ASCII.GetBytes("RCPT TO:<" + addressEmail + ">" + CRLF);
                netStream.Write(dataBuffer, 0, dataBuffer.Length);
                ResponseString = reader.ReadLine();
                if (GetResponseCode(ResponseString) == 550)
                {
                    tClient.Close();
                    return "Email <" + addressEmail + "> does not exist!" + ResponseString;
                }
                else
                {
                    tClient.Close();
                    return "OK";
                }
            }
            else
            {
                return "Email <" + addressEmail + "> not a email";
            }    
        }
        private int GetResponseCode(string ResponseString)
        {
            return int.Parse(ResponseString.Substring(0, 3));
        }

        public string CheckAuthencation(string addressEmail, string pass)
        {
            try
            {
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(addressEmail, pass);
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        static Regex ValidEmailRegex = CreateValidEmailRegex();
        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }

        internal static bool EmailIsValid(string emailAddress)
        {
            bool isValid = ValidEmailRegex.IsMatch(emailAddress);

            return isValid;
        }

        public async Task<ListResultDto<SenderLookup>> GetSenderLookupAsync()
        {
            var sender = await _senderEmailRepository.GetListAsync();
            var senderEmailLookup = ObjectMapper.Map<List<SenderEmail>, List<SenderLookup>>(sender);
            return new ListResultDto<SenderLookup>(
                    senderEmailLookup
                );
        }

        public async Task<PagedResultDto<ContentEmailDto>> GetListAsync(GetContentEmailInput input)
        {
            string[] separatingStrings = { "</p>", "<p>" };
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(ContentEmail.CreationTime);
            }

            var contenEmails = await _ContentEmailRepository.GetListAsync(
                    input.SkipCount,
                    input.MaxResultCount,
                    input.Sorting,
                    input.Filter
                );

            
            var contenEmailDtos = ObjectMapper.Map<List<ContentEmail>, List<ContentEmailDto>>(contenEmails);
            var toalCount = await _ContentEmailRepository.GetCountAsync();

            foreach (var item in contenEmailDtos)
            {
                var bodySplit = item.Body.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
                item.BodyShow = bodySplit;
                item.CustomerName = await GetCustomerNameAsync(item);
                item.SenderEmail = await GetEmailAsync(item);
            }

            return new PagedResultDto<ContentEmailDto>(
                    toalCount,
                    contenEmailDtos
                 );
        }

        private async Task<string> GetCustomerNameAsync(ContentEmailDto contentEmailDto)
        {
            var userName = _currentUser.Name;
            string customerName;
            if (userName != "admin")
            {
                var customer = await _customerRepository.FindByIdAsync(contentEmailDto.CustomerID);
                customerName = customer.FullName;
            }
            else {
                customerName = _currentUser.Name;
            }
            
            
            return customerName;
        }
        private async Task<string> GetEmailAsync(ContentEmailDto contentEmailDto)
        {
            var sender = await _senderEmailRepository.FindByIdAsync(contentEmailDto.SenderEmailID);
            var senderEmail = sender.Email;
            return senderEmail;
        }

    }
}
