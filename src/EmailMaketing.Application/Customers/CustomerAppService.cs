using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EmailMaketing.Customers
{
    public class CustomerAppService : ApplicationService, ICustomerAppService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerAppService(ICustomerRepository customerRepository )
        {
            _customerRepository = customerRepository;
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
    }
}
