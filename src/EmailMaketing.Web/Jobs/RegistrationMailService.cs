
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

        public async Task RegisterAsync(string emailAddress)
        {
            await _backgroundJobManager.EnqueueAsync<SendEmailArgs>(
                new SendEmailArgs
                {
                    EmailAddress= emailAddress,
                    Subject = "You've successfully registered!",
                    Body = "this is gmail"
                }, BackgroundJobPriority.High, TimeSpan.FromSeconds(1)
            );
        }
    }
}
