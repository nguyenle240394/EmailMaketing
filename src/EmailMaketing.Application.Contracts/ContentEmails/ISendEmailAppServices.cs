using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailMaketing.ContentEmails
{
    public interface ISendEmailAppServices
    {
        Task SendMail(BodyEmail body, string emailaddress, string name, string pass, List<string> listfile);
        Task SendEmailAsync(string email, string subject, string htmlMessage, string name, string emailaddress, string pass, List<string> listfile);
        string CheckEmail(string Address, string pass);
    }
}
