using EmailMaketing.ContentEmails;
using EmailMaketing.SenderEmails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Users;

namespace EmailMaketing.Web.Pages.ContentEmails
{
    public class SendEmailModalModel : EmailMaketingPageModel
    {
        private readonly ContentEmailAppService _contentEmailAppService;
        private readonly ICurrentUser _currentUser;
        private readonly SenderEmailAppService _senderEmailAppService;

        [BindProperty]
        public CreateContentViewModal ContentEmail { get; set; }
        public List<SelectListItem> SenderEmails { get; set; }
        public IFormFile File { get; set; }

        public SendEmailModalModel(ContentEmailAppService contentEmailAppService,
            ICurrentUser currentUser,
            SenderEmailAppService senderEmailAppService)
        {
            _contentEmailAppService = contentEmailAppService;
            _currentUser = currentUser;
            _senderEmailAppService = senderEmailAppService;
        }

        public async Task OnGetAsync()
        {
            ContentEmail = new CreateContentViewModal();
            var senderLookup = await _contentEmailAppService.GetSenderLookupAsync();
            SenderEmails = senderLookup.Items
                .Select(s => new SelectListItem(s.email, s.Id.ToString()))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var listEmailReceive = ContentEmail.RecipientEmail.ToString().Split('\r', '\n');
            foreach (var item in ContentEmail.SenderEmailId)
            {
                var sender = await _senderEmailAppService.GetSenderEmailAsync(item);
                if (listEmailReceive.Length > 0 && listEmailReceive != null)
                {
                    string htmlbody = "";
                    var linesbody = ContentEmail.Body.ToString().Split('\r', '\n');
                    foreach (var line in linesbody)
                    {
                        if (line != "")
                        {
                            htmlbody += "<p>" + line + "</p>";
                        }

                    }
                    htmlbody += "<p style='display:none'>" + randomtext() + "</p>";
                    var countEmailReceive = listEmailReceive.Length;
                    for (int i = 0; i < countEmailReceive; i++)
                    {
                        if (listEmailReceive[i] != "")
                        {
                            /*await _contentEmailAppService.SendMailAsync(
                            listEmailReceive[i],
                            ContentEmail.Subject,
                            htmlbody,
                            sender.Email,
                            ContentEmail.Name,
                            sender.Password,
                            File);*/
                        }

                    }
                }
            }


            return RedirectToAction("SendEmailModal", "ContentEmails");
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

        public class CreateContentViewModal
        {
            public string Name { get; set; }
            [Required]
            public string Subject { get; set; }
            [Required]
            [TextArea(Rows = 10)]
            public string Body { get; set; }
            [Required]
            [TextArea(Rows = 3)]
            [DisplayName("Recipient Email")]
            public string RecipientEmail { get; set; }
            [DisplayName("File")]
            public List<string> Attachment { get; set; }
            public bool Status { get; set; }
            public bool Featured { get; set; }
            public Guid CustomerID { get; set; }
            public DateTime Schedule { get; set; }

            [Required]
            [SelectItems(nameof(SenderEmails))]
            [DisplayName("Sender Email")]
            public List<Guid> SenderEmailId { get; set; }
        }
    }
}
