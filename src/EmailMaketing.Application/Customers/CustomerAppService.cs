using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace EmailMaketing.Customers
{
    public class CustomerAppService : ApplicationService, ICustomerAppService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IdentityUserAppService _identityUserAppService;
        private readonly IdentityUserManager _identityUserManager;

        public CustomerAppService(ICustomerRepository customerRepository, IdentityUserAppService identityUserAppService)
        {
            _customerRepository = customerRepository;
            _identityUserAppService = identityUserAppService;
        }

        public async Task ChangeStatus(Guid Id)
        {
            var identityUser = new IdentityUserUpdateDto();
            var customer = await _customerRepository.FindAsync(Id);

            customer.Status = !customer.Status;
            await _customerRepository.UpdateAsync(customer);
            var user = await _identityUserAppService.GetAsync(customer.UserID);

            user.IsActive = !user.IsActive;
            identityUser.UserName= user.UserName;
            identityUser.Email = user.Email;
            identityUser.IsActive = user.IsActive;
            await _identityUserAppService.UpdateAsync(customer.UserID, identityUser);
        }

        public async Task<CustomerDto> CreateAsync(CreateUpdateCustomer input)
        {
            var customer = ObjectMapper.Map<CreateUpdateCustomer, Customer>(input);
            await _customerRepository.InsertAsync(customer);
            return ObjectMapper.Map<Customer, CustomerDto>(customer);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var customer = await _customerRepository.FindAsync(id);
            return true;
        }

        public Task<CustomerDto> GetCustomerAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResultDto<CustomerDto>> GetListAsync(GetCustomerInput input)
        {
            var customers = await _customerRepository.GetListAsync(
                    input.SkipCount,
                    input.MaxResultCount,
                    input.Sorting,
                    input.Filter
                );
            
            var customerAdd = ObjectMapper.Map<List<Customer>, List<CustomerDto>>(customers);
            var totalCount = await _customerRepository.GetCountAsync();
            return new PagedResultDto<CustomerDto>(
                    totalCount,
                    customerAdd
                );
        }

        public Task<CustomerDto> UpdateAsync(Guid id, CreateUpdateCustomer input)
        {
            throw new NotImplementedException();
        }
    }
}
