using EmailMaketing.Customers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace EmailMaketing.Web.Pages.Customers
{
    public class CreateModalModel : EmailMaketingPageModel
    {
        private readonly ICustomerAppService _customerAppService;
        private readonly IdentityUserAppService _identityUserAppService;

        [BindProperty]
        public CreateUpdateCustomer Customer { get; set; }
        [BindProperty]
        public IdentityUserCreateDto AppUser { get; set; }
        public IdentityUser identityuser { get; set; }
        public CreateModalModel(ICustomerAppService customerAppService, IdentityUserAppService identityUserAppService)
        {
            _customerAppService = customerAppService;
            _identityUserAppService = identityUserAppService;
            
        }
        public void OnGet()
        {
            Alerts.Warning(
               text: "We will have a service interruption between 02:00 AM and 04:00 AM at October 23, 2023!",
               title: "Service Interruption"
           );
        }
        public async Task<IActionResult> OnPostAsync()
        {
            /*await _identityUserAppService.CreateAsync(AppUser);*/
            var userNameId = await _identityUserAppService.FindByUsernameAsync(AppUser.UserName);
            Customer.UserID = userNameId.Id;
            var emailExist = await CheckEmailExist(AppUser.Email);
            if (emailExist)
            {
                Alerts.Warning(
                text: "We will have a service interruption between 02:00 AM and 04:00 AM at October 23, 2023!",
                title: "Service Interruption"                
                );
                return Page();
            }
            /*Customer.Email = userNameId.Email;*/
            await _customerAppService.CreateAsync(Customer);
            return RedirectToAction("Index", "Customers");
        }


        public async Task<bool>  CheckEmailExist(string email)
        {
            var emailExist = await _identityUserAppService.FindByEmailAsync(email);
            if (emailExist != null)
            {
                return true;
            }
            return false;
        }

    }
}
