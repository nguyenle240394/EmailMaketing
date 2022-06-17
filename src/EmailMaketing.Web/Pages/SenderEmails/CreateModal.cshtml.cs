using AutoMapper.Internal.Mappers;
using EmailMaketing.SenderEmails;
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

namespace EmailMaketing.Web.Pages.SenderEmails
{
    public class CreateModalModel : PageModel
    {
        private readonly ISenderEmailAppService _senderEmailAppService;
        public CreateModalModel(ISenderEmailAppService senderEmailAppService)
        {
            _senderEmailAppService = senderEmailAppService;
        }

        [BindProperty]
        public CreateUpdateSenderEmailDto SenderEmail { get; set; }

        public async Task OnGet()
        {
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            await _senderEmailAppService.CreateAsync(SenderEmail);
            return RedirectToAction("Index", "SenderEmails");
        }
    }
}
