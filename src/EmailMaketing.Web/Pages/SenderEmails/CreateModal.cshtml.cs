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
    public class CreateModalModel : EmailMaketingPageModel
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
        public CreateSenderEmailViewModal SenderEmail { get; set; }

        public void OnGet()
        {
            SenderEmail = new CreateSenderEmailViewModal();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(_currentUser.UserName != "admin")
            {
                var userId = _currentUser.Id; // Lay userId hien tai
                var customer = await _customerRepository.FindAsync(x => x.UserID == userId);
                SenderEmail.CustomerID = customer.Id;
            }
            else
            {
                SenderEmail.CustomerID = null;
            }
            var senderemails = ObjectMapper.Map<CreateSenderEmailViewModal, CreateUpdateSenderEmailDto>(SenderEmail);
            await _senderEmailAppService.CreateAsync(senderemails);
            return NoContent();
        }

        public class CreateSenderEmailViewModal
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }
            [HiddenInput]
            public Guid? CustomerID { get; set; }
        }
    }
}
