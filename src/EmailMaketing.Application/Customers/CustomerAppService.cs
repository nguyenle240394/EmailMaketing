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
            var customer = await _customerRepository.FindAsync(Id);
            customer.Status = !customer.Status;
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task<CustomerDto> CreateAsync(CreateUpdateCustomer input)
        {
            var customer = ObjectMapper.Map<CreateUpdateCustomer, Customer>(input);
            await _customerRepository.InsertAsync(customer);
            return ObjectMapper.Map<Customer, CustomerDto>(customer);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
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


        /*private async Task<Dictionary<Guid, IdentityUser>> GetSupplierDictionaryAsync(List<Customer> customers)
        {
            var identityUserIds = customers
                .Select(c => c.UserID)
                .Distinct()
                .ToArray();

            var queryable = await _identityUser.
            var suppliers = await AsyncExecuter.ToListAsync(queryable.Where(s => supplierIds.Contains(s.Id)));
            return suppliers.ToDictionary(x => x.Id, x => x);
        }*/
    }
}
