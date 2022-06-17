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

        public async Task<SenderEmailDto> GetAsync(Guid id)
        {
            var sender = await _senderEmailRepository.GetAsync(id);
            return ObjectMapper.Map<SenderEmail, SenderEmailDto>(sender);
        }

        public async Task<SenderEmailDto> CreateAsync(CreateUpdateSenderEmailDto input)
        {
            var SenderEmail = ObjectMapper.Map<CreateUpdateSenderEmailDto, SenderEmail>(input);
            await _senderEmailRepository.InsertAsync(SenderEmail);
            return ObjectMapper.Map<SenderEmail, SenderEmailDto>(SenderEmail);
        }

        //public async Task<bool> DeleteAsync(Guid id)
        //{
        //    var items = await _senderEmailRepository.FindAsync(id);
        //    await _senderEmailRepository.DeleteAsync(items);
        //    return true;
        //}

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
            //Get a lookup dictionary for the related authors
            //var customerDictionary = await GetCustomerDictionaryAsync(senderemail);
            //Set AuthorName for the DTOs
            //senderEmailDtos.ForEach(senderEmailDto => senderEmailDto.CustomerName = customerDictionary[senderEmailDto.CustomerID].FullName);
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
            //Get a lookup dictionary for the related authors
            //var customerDictionary = await GetCustomerDictionaryAsync(senderemail);
            //Set AuthorName for the DTOs
            //senderEmailDtos.ForEach(senderEmailDto => senderEmailDto.CustomerName = customerDictionary[senderEmailDto.CustomerID].FullName);
            //Get the total count with another query (required for the paging)
            var totalcount = await _senderEmailRepository.GetCountAsync();
            return new PagedResultDto<SenderWithNavigationDto>
            {
                TotalCount = totalcount,
                Items = senderWithNavigationDtos
            };
        }
        public async Task<ListResultDto<CustomerLookupDto>> GetCustomerLookupAsync()
        {
            var customers = await _customerRepository.GetListAsync();
            return new ListResultDto<CustomerLookupDto>(
                ObjectMapper.Map<List<Customer>, List<CustomerLookupDto>>(customers));
        }

        public async Task<Dictionary<Guid, Customer>> GetCustomerDictionaryAsync(List<SenderEmail> senderEmails)
        {
            var customerId = senderEmails
                .Select(s => s.CustomerID)
                .Distinct()
                .ToArray();
            var queryable = await _customerRepository.GetQueryableAsync();
            var customers = await AsyncExecuter.ToListAsync(queryable.Where(c => customerId.Contains(c.Id)));
            return customers.ToDictionary(x => x.Id, x => x);
        }


        //public async Task<SenderEmailDto> UpdateAsync(Guid id, CreateUpdateSenderEmailDto input)
        //{
        //    var items = await _senderEmailRepository.FindAsync(id);
        //    items.Email = input.Email;
        //    items.Password = input.Password;
        //    await _senderEmailRepository.UpdateAsync(items);
        //    return ObjectMapper.Map<SenderEmail, SenderEmailDto>(items);
        //}
    }
}
