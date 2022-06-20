using EmailMaketing.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace EmailMaketing.Web.Pages.SenderEmails
{
    
    public class EditModalModel : EmailMaketingPageModel
    {
        private readonly CustomerAppService _customerAppService;

        [BindProperty]
        public EditCustomerViewModal Customer { get; set; }

        public EditModalModel(CustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
        }
        public async Task OnGetAsync(Guid id)
        {
            var customerDto = await _customerAppService.GetCustomerAsync(id);
            Customer = ObjectMapper.Map<CustomerDto, EditCustomerViewModal>(customerDto);

        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _customerAppService.UpdateAsync(
                    Customer.Id,
                    ObjectMapper.Map<EditCustomerViewModal, CreateUpdateCustomer>(Customer)
                );
            return NoContent();
        }
        public class EditCustomerViewModal
        {
            [HiddenInput]
            [BindProperty(SupportsGet = true)]
            public Guid Id { get; set; }
            [HiddenInput]
            public Guid UserID { get; set; }
            [DisplayName("User Name")]
            public string UserName { get; set; }
            public string Password { get; set; }
            [DisplayName("Full Name")]
            /*[RegularExpression("[a-zA-Z]Vs")]*/
            public string FullName { get; set; }
            [RegularExpression("[0-9]{10}")]
            public string PhoneNumber { get; set; }
            [EmailAddress]
            public string Email { get; set; }
            /*public bool Status { get; set; }*/
        }
    }
}
