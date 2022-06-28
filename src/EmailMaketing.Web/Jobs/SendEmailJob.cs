
using EmailMaketing.ContentEmails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace EmailMaketing.Jobs
{
    public class SendEmailJob : AsyncBackgroundJob<SendEmailArgs>, ITransientDependency
    {
        private readonly ContentEmailAppService _contentEmailAppService;

        public SendEmailJob(ContentEmailAppService contentEmailAppService)
        {
            _contentEmailAppService = contentEmailAppService;
        }
        public override async Task ExecuteAsync(SendEmailArgs args)
        {
            await _contentEmailAppService.SendMailAsync(
                    args.To,
                    args.Subject,
                    args.Body,
                    args.EmailAddress,
                    args.Name,
                    args.Password,
                    args.File
                );
        }
    }
}
