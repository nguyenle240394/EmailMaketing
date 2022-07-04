using EmailMaketing.ContentEmails;
using EmailMaketing.SenderEmails;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
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
        private readonly IHostingEnvironment _hostingEnvironment;

        [BindProperty]
        public CreateContentViewModal ContentEmail { get; set; }
        private List<string> listsfile = new List<string>();
        [BindProperty]
        public IFormFile FileUpload { get; set; }
        public SendEmailModalModel(ContentEmailAppService contentEmailAppService,
            ICurrentUser currentUser,
            SenderEmailAppService senderEmailAppService,
            IHostingEnvironment hostingEnvironment)
        {
            _contentEmailAppService = contentEmailAppService;
            _currentUser = currentUser;
            _senderEmailAppService = senderEmailAppService;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task OnGetAsync()
        {
            ContentEmail = new CreateContentViewModal();
            /*var senderLookup = await _contentEmailAppService.GetSenderLookupAsync();*/
            /*SenderEmails = senderLookup.Items
                .Select(s => new SelectListItem(s.email, s.Id.ToString()))
                .ToList();*/
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //get data to form va cat cac phan tu \r, \n
            var listEmailReceive = ContentEmail.RecipientEmail.ToString().Split('\r', '\n');
            // tao bien de remove khoi arry
            string stringToRemove = "";
            // remove cac phan tu ""
            listEmailReceive = listEmailReceive.Where(val => val != stringToRemove).ToArray();
            var senderIsSendFalse = new SenderEmailDto();
            if (FileUpload != null)
            {
                //luu file vao thu muc wwwroot/FilesUpload
                var file = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot/FilesUpload", FileUpload.FileName);
                listsfile.Add(file);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await FileUpload.CopyToAsync(fileStream);
                }
            }
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
                    senderIsSendFalse = await _senderEmailAppService.SenderIsSendFalseAsync();
                    if (senderIsSendFalse == null)
                    {
                        await _senderEmailAppService.ChangeIsSendToFalseAsync();
                        senderIsSendFalse = await _senderEmailAppService.SenderIsSendFalseAsync();
                    }
                    if (listEmailReceive[i] != "")
                    {
                        await _contentEmailAppService.SendMailAsync(
                        listEmailReceive[i],
                        ContentEmail.Subject,
                        htmlbody,
                        senderIsSendFalse.Email,
                        ContentEmail.Name,
                        senderIsSendFalse.Password,
                        listsfile);
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
        }
    }
}
