﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EmailMaketing.ContentEmails
{
    public interface IContentEmailAppService : IApplicationService
    {
        Task<ContentEmailDto> CreateAsync(CreateUpdateContentEmailDto input);
        Task<List<ContentEmailDto>> GetListEmailAsync(Guid id);
        Task<ContentEmailDto> GetEmailAsync(Guid id);

        Task<bool> DeleteAsync(Guid id);

        Task<ContentEmailDto> UpdateDataAsync(Guid id, ContentEmailDto input);
        //Task<ContentEmailDto> UpdateAsync(Guid)
    }
}
 