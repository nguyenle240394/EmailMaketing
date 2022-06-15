﻿using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EmailMaketing.Customers
{
    public interface ICustomerAppService : IApplicationService
    {
        Task<PagedResultDto<CustomerDto>> GetListAsync(GetCustomerInput input);
        Task<CustomerDto> CreateAsync(CreateUpdateCustomer input);
        Task<bool> DeleteAsync(Guid id);
        Task<CustomerDto> UpdateAsync(Guid id, CreateUpdateCustomer input);
        Task<CustomerDto> GetCustomerAsync(Guid id);
    }
}
