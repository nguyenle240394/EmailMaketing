using AutoMapper.Internal.Mappers;
using EmailMaketing.Customers;
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
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace EmailMaketing.Web.Pages.SenderEmails
{
    public class CreateModalModel : PageModel
    {
        private readonly ISenderEmailAppService _senderEmailAppService;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICurrentUser _currentUser;
        public CreateModalModel(
            ISenderEmailAppService senderEmailAppService, ICurrentUser currentUser, ICustomerRepository customerRepository)
        {
            _senderEmailAppService = senderEmailAppService;
            _currentUser = currentUser;
            _customerRepository = customerRepository;
        }

        [BindProperty]
        public CreateUpdateSenderEmailDto SenderEmail { get; set; }

        public async Task OnGetAsync()
        {
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _currentUser.Id; // Lay userId hien tai
            var customer = await _customerRepository.FindAsync(x => x.UserID == userId);
            SenderEmail.CustomerID = customer.Id;
            await _senderEmailAppService.CreateAsync(SenderEmail);
            return RedirectToAction("Index", "SenderEmails");
        }
    }
}
