﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EmailMaketing.SenderEmails
{
    public interface ISenderEmailAppService : IApplicationService
    {
        Task<SenderEmailDto> GetAsync(Guid id);
        Task<PagedResultDto<SenderEmailDto>> GetListAsync(GetSenderEmailInput input);
        Task<SenderEmailDto> CreateAsync(CreateUpdateSenderEmailDto input);
        //Task<SenderEmailDto> UpdateAsync(Guid id, CreateUpdateSenderEmailDto input);
        //Task<bool> DeleteAsync(Guid id);
        Task<ListResultDto<CustomerLookupDto>> GetCustomerLookupAsync();
        

    }
}
