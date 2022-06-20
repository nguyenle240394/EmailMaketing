using EmailMaketing.Customers;
using EmailMaketing.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;

namespace EmailMaketing.SenderEmails
{
    //[Authorize(EmailMaketingPermissions.SenderEmails.Default)]
    public class SenderEmailAppService : ApplicationService, ISenderEmailAppService
    {
        private readonly ISenderEmailRepository _senderEmailRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IIdentityUserRepository _identityUserRepository;
        public SenderEmailAppService(
            ISenderEmailRepository senderEmailRepository,
            ICustomerRepository customerRepository,
            IIdentityUserRepository identityUserRepository)
        {
            _senderEmailRepository = senderEmailRepository;
            _customerRepository = customerRepository;
            _identityUserRepository = identityUserRepository;
        }

        public async Task<SenderEmailDto> CreateAsync(CreateUpdateSenderEmailDto input)
        {
            var SenderEmail = ObjectMapper.Map<CreateUpdateSenderEmailDto, SenderEmail>(input);
            await _senderEmailRepository.InsertAsync(SenderEmail);
            return ObjectMapper.Map<SenderEmail, SenderEmailDto>(SenderEmail);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var items = await _senderEmailRepository.FindAsync(id);
            await _senderEmailRepository.DeleteAsync(items);
            return true;
        }

        public async Task<PagedResultDto<SenderEmailDto>> GetListAsync(GetSenderEmailInput input)
        {
            //Set a default sorting, if not provided
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(SenderEmail.Email);
            }

            
            var senderemail = await _senderEmailRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter);
            //Convert to DTOs
            var senderEmailDtos = ObjectMapper.Map<List<SenderEmail>, List<SenderEmailDto>>(senderemail);
            //Get the total count with another query (required for the paging)
            var totalcount = await _senderEmailRepository.GetCountAsync();
            return new PagedResultDto<SenderEmailDto>
            {
                TotalCount = totalcount,
                Items = senderEmailDtos
            };
        }

        public async Task<PagedResultDto<SenderWithNavigationDto>> GetListWithNavigationAsync(GetSenderEmailInput input)
        {
            //Set a default sorting, if not provided
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(SenderEmail.Email);
            }


            var senderemail = await _senderEmailRepository.GetListWithNavigationAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter);
            //Convert to DTOs
            var senderWithNavigationDtos = ObjectMapper.Map<List<SenderWithNavigation>, List<SenderWithNavigationDto>>(senderemail);
            //Get the total count with another query (required for the paging)
            var totalcount = await _senderEmailRepository.GetCountAsync();
            return new PagedResultDto<SenderWithNavigationDto>
            {
                TotalCount = totalcount,
                Items = senderWithNavigationDtos
            };
        }

        public async Task<SenderEmailDto> GetSenderEmailAsync(Guid Id)
        {
            var senderemail = await _senderEmailRepository.FindAsync(Id);
            return ObjectMapper.Map<SenderEmail, SenderEmailDto>(senderemail);
        }

        public async Task<SenderEmailDto> UpdateAsync(Guid id, CreateUpdateSenderEmailDto input)
        {
            var items = await _senderEmailRepository.FindAsync(id);
            items.Email = input.Email;
            items.Password = input.Password;
            items.CustomerID = input.CustomerID;
            await _senderEmailRepository.UpdateAsync(items);
            return ObjectMapper.Map<SenderEmail, SenderEmailDto>(items);
        }
    }
}
