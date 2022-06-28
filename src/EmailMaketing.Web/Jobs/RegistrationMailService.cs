
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;

namespace EmailMaketing.Jobs
{
    public class RegistrationMailService : ApplicationService
    {
        private readonly IBackgroundJobManager _backgroundJobManager;

        public RegistrationMailService(IBackgroundJobManager backgroundJobManager)
        {
            _backgroundJobManager = backgroundJobManager;
        }

        public async Task RegisterAsync()
        {
            string htmlbody = "";
            htmlbody = "<p>" + randomtext() + "<p>";

            var listEmail = new List<SendEmailArgs>();
            listEmail.Add(new SendEmailArgs {
                To = "nguyenle.httdn@gmail.com",
                Subject = "Test gửi 100 email",
                Body = "Ráng nhận tinh nhắn nha sếp " + htmlbody,
                EmailAddress = "HenryDao0810@gmail.com",
                Name = "Nguyen le",
                Password = "leuzxdmiwryorxxi",
                File = new List<string>()
            });

            listEmail.Add(new SendEmailArgs
            {
                To = "phongnguyen.httdn@gmail.com",
                Subject = "Test gửi 100 email",
                Body = "Ráng nhận tinh nhắn nha sếp " + htmlbody,
                EmailAddress = "HenryDao0810@gmail.com",
                Name = "Nguyen le",
                Password = "leuzxdmiwryorxxi",
                File = new List<string>()
            });

            listEmail.Add(new SendEmailArgs
            {
                To = "letg3313@gmail.com",
                Subject = "Test gửi 100 email",
                Body = "Ráng nhận tinh nhắn nha sếp " + htmlbody,
                EmailAddress = "HenryDao0810@gmail.com",
                Name = "Nguyen le",
                Password = "leuzxdmiwryorxxi",
                File = new List<string>()
            });

            foreach (var item in listEmail)
            {
                await _backgroundJobManager.EnqueueAsync(item, BackgroundJobPriority.High, TimeSpan.FromSeconds(5));
                await Task.Delay(30000);
            }
            
        }
        private string randomtext()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[30];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }
    }
}
