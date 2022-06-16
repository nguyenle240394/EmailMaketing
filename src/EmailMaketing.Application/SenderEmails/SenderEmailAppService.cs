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
using Volo.Abp.ObjectMapping;

namespace EmailMaketing.SenderEmails
{
    [Authorize(EmailMaketingPermissions.SenderEmails.Default)]
    public class SenderEmailAppService : ApplicationService, ISenderEmailAppService
    {
        private readonly ISenderEmailRepository _senderEmailRepository;
        private readonly ICustomerRepository _customerRepository;
        public SenderEmailAppService(
            ISenderEmailRepository senderEmailRepository,
            ICustomerRepository customerRepository)
        {
            _senderEmailRepository = senderEmailRepository;
            _customerRepository = customerRepository;
        }

        public async Task<SenderEmailDto> CreateAsync(CreateUpdateSenderEmailDto input)
        {
            var items = ObjectMapper.Map<CreateUpdateSenderEmailDto, SenderEmail>(input);
            await _senderEmailRepository.InsertAsync(items);
            return ObjectMapper.Map<SenderEmail, SenderEmailDto>(items);
        }

        //public async Task<bool> DeleteAsync(Guid id)
        //{
        //    var items = await _senderEmailRepository.FindAsync(id);
        //    await _senderEmailRepository.DeleteAsync(items);
        //    return true;
        //}

        public async Task<ListResultDto<CustomerLookupDto>> GetCustomerLookupAsync()
        {
            var customers = await _customerRepository.GetListAsync();
            var customerLookupDto = ObjectMapper.Map<List<Customer>, List<CustomerLookupDto>>(customers);
            return new ListResultDto<CustomerLookupDto>(customerLookupDto);
        }

        private async Task<Dictionary<Guid, Customer>> GetCustomerDictionaryAsync(List<SenderEmail> senderEmails)
        {
            var customerId = senderEmails
                .Select(s => s.CustomerID)
                .Distinct()
                .ToArray();
            var query = await _customerRepository.GetQueryableAsync();
            var customers = await AsyncExecuter.ToListAsync(query.Where(c => customerId.Contains(c.Id)));
            return customers.ToDictionary(x => x.Id, x => x);
        }

        public async Task<PagedResultDto<SenderEmailDto>> GetListAsync(GetSenderEmailInput input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(SenderEmail.Email);
            }
            var senderemail = await _senderEmailRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter);
            var totalcount = await _senderEmailRepository.GetCountAsync();
            var senderEmailDtos = ObjectMapper.Map<List<SenderEmail>, List<SenderEmailDto>>(senderemail);
            var customerDictionary = await GetCustomerDictionaryAsync(senderemail);
            senderEmailDtos.ForEach(senderEmailDto => senderEmailDto.CustomerName = customerDictionary[senderEmailDto.CustomerID].FullName);
            
            return new PagedResultDto<SenderEmailDto>(
                totalcount,
                senderEmailDtos
            );
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
