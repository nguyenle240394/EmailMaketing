using EmailMaketing.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Identity;

namespace EmailMaketing.Web.Pages.Customers
{
    public class CreateRoleModalModel : EmailMaketingPageModel
    {
        private readonly IdentityRoleAppService _identityRoleAppService;
        private readonly IdentityUserAppService _identityUserAppService;
        private readonly CustomerAppService _customerAppService;

        [BindProperty]
        public EditCustomerRolesViewModal CustomerRole { get; set; }
        
        public List<SelectListItem> Roles { get; set; }
        [BindProperty]
        public IdentityUserUpdateRolesDto UpdateRole { get; set; }
        public CreateRoleModalModel(IdentityRoleAppService identityRoleAppService, IdentityUserAppService identityUserAppService,
            CustomerAppService customerAppService)
        {
            _identityRoleAppService = identityRoleAppService;
            _identityUserAppService = identityUserAppService;
            _customerAppService = customerAppService;
        }
        public async Task OnGetAsync(Guid id)
        {
            var customerDto = await _customerAppService.GetCustomerAsync(id);
            CustomerRole = ObjectMapper.Map<CustomerDto, EditCustomerRolesViewModal>(customerDto);
            var roles = await _identityRoleAppService.GetAllListAsync();

            Roles = roles.Items
                .Select(r => new SelectListItem(r.Name, r.Id.ToString()))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // get all roles
            var role = await _identityRoleAppService.GetAsync(CustomerRole.RoleID);

            // set value for IdentityUserUpdateRolesDto
            UpdateRole.RoleNames = new string[] { role.Name };

            // Update Roles for user
            await _identityUserAppService.UpdateRolesAsync(CustomerRole.UserID, UpdateRole);
            return NoContent();
        }
        public class EditCustomerRolesViewModal
        {
            [SelectItems(nameof(Roles))]
            [DisplayName("Roles")]
            public Guid RoleID { get; set; }
            [HiddenInput]
            [BindProperty(SupportsGet = true)]
            public Guid Id { get; set; }
            [HiddenInput]
            public Guid UserID { get; set; }
        }
    }
}
