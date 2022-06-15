using EmailMaketing.Customers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace EmailMaketing.Web.Pages.Customers
{
    public class CreateModalModel : EmailMaketingPageModel
    {
        private readonly ICustomerAppService _customerAppService;
        [BindProperty]
        public CreateUpdateCustomer Customer { get; set; }
        public CreateModalModel(ICustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync(/*IFormCollection formCollection*/)
        {
            await _customerAppService.CreateAsync(Customer);
            return RedirectToAction("Index", "Customers");
        }
    }
}
