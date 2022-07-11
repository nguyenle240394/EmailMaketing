using EmailMaketing.ContentEmails;
using EmailMaketing.Customers;
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
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Identity;

namespace EmailMaketing.Web.Pages.Customers
{
    public class CreateModalModel : EmailMaketingPageModel
    {
        private readonly ICustomerAppService _customerAppService;
        private readonly IdentityUserAppService _identityUserAppService;
        private readonly ContentEmailAppService _contentEmailAppService;
        private readonly IdentityRoleAppService _identityRoleAppService;

        [BindProperty]
        public CreateCustomerViewModal Customer { get; set; }
        [BindProperty]
        public IdentityUserCreateDto AppUser { get; set; }

        /*public List<SelectListItem> CustomerTypes { get; set; }*/
        public CreateModalModel(ICustomerAppService customerAppService, IdentityUserAppService identityUserAppService,
           ContentEmailAppService contentEmailAppService, IdentityRoleAppService identityRoleAppService )
        {
            _customerAppService = customerAppService;
            _identityUserAppService = identityUserAppService;
            _contentEmailAppService = contentEmailAppService;
            _identityRoleAppService = identityRoleAppService;
        }
        public async void OnGet()
        {
            AppUser = new IdentityUserCreateDto();
            Customer = new CreateCustomerViewModal();
            /*var customerLookup = await _customerAppService.GetCustomerTypeLookupAsync();
            CustomerTypes = customerLookup.Items
                .Select(c => new SelectListItem(c.Name, c.Id.ToString()))
                .ToList();*/
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var pass = Request.Form["password"];
            Customer.Password = pass;
            var userExist = await _identityUserAppService.FindByEmailAsync(Customer.UserName);
            var emailExist = await _identityUserAppService.FindByEmailAsync(Customer.Email);
            if (userExist != null)
            {
                throw new UserFriendlyException(L["User Name is already exists"]);
            }
            if (emailExist != null)
            {
                throw new UserFriendlyException(L["Email is already exists"]);
            }
            AppUser.UserName = Customer.UserName;
            AppUser.Password = Customer.Password;
            AppUser.Email = Customer.Email;

            await _identityUserAppService.CreateAsync(AppUser);

            var userId = await _identityUserAppService.FindByUsernameAsync(AppUser.UserName);
            Customer.UserID = userId.Id;
            await _customerAppService.CreateAsync(
                    ObjectMapper.Map<CreateCustomerViewModal, CreateUpdateCustomer>(Customer)
                );
            return NoContent();
        }

        public class CreateCustomerViewModal
        {
            [HiddenInput]
            public Guid UserID { get; set; }
            [Required]
            public CustomerType Type { get; set; }
            [Required]
            [DisplayName("User Name")]
            public string UserName { get; set; }
            [Required]
            public string Password { get; set; }
            [Required]
            [DisplayName("Full Name")]
            /*[RegularExpression("[a-zA-Z]Vs")]*/
            public string FullName { get; set; }
            [Required]
            [RegularExpression("[0-9]{10}")]
            [DisplayName("Phone Number")]
            public string PhoneNumber { get; set; }
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            /*public bool Status { get; set; }*/
        }

    }
}
