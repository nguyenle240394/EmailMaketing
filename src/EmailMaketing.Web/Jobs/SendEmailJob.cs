
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
        private readonly ISendMailService _emailSender;

        public SendEmailJob(ISendMailService emailSender)
        {
            _emailSender = emailSender;
        }
        public override async Task ExecuteAsync(SendEmailArgs args)
        {
            await _emailSender.SendEmailAsync(
                args.EmailAddress,
                args.Subject,
                args.Body
                );
        }
    }
}
