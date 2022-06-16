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
        public SenderEmailAppService(ISenderEmailRepository senderEmailRepository)
        {
            _senderEmailRepository = senderEmailRepository;
        }

        public async Task<SenderEmailDto> CreateAsync(CreateUpdateSenderEmailDto input)
        {
            var items = ObjectMapper.Map<CreateUpdateSenderEmailDto, SenderEmail>(input);
            await _senderEmailRepository.InsertAsync(items);
            return ObjectMapper.Map<SenderEmail, SenderEmailDto>(items);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var items = await _senderEmailRepository.FindAsync(id);
            await _senderEmailRepository.DeleteAsync(items);
            return true;
        }

        //public async Task<ListResultDto<CustomerLookupDto>> GetCustomerLookupAsync()
        //{
        //    var customers = await _senderEmailRepository.GetListAsync();
        //    var customerLookupDto = ObjectMapper.Map<List<Customer>, List<CustomerLookupDto>>(customers);
        //    return new ListResultDto<CustomerLookupDto>(customerLookupDto);
        //}

        public async Task<PagedResultDto<SenderEmailDto>> GetListAsync(GetSenderEmailInput input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(SenderEmail.Email);
            }
            var sendemail = await _senderEmailRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter);
            var totalcount = await _senderEmailRepository.GetCountAsync();
            return new PagedResultDto<SenderEmailDto>(
                totalcount,
                ObjectMapper.Map<List<SenderEmail>, List<SenderEmailDto>>(sendemail)
            );
        }

        //public async Task<List<SelectListItems<Guid?>>> GetListSendEmailLookupAsync(Guid? Id = null)
        //{
        //    var items = await _senderEmailRepository.GetListSendEmailLookupAsync(Id);
        //    List<SelectListItems<Guid?>> ListLookupDto = new List<SelectListItems<Guid?>>();
        //    foreach (var item in items)
        //    {
        //        var lookupDto = new SelectListItems<Guid?>();
        //        lookupDto.Id = item.Id;
        //        lookupDto.DisplayName = item.Email;
        //        ListLookupDto.Add(lookupDto);
        //    }
        //    return ListLookupDto;
        //}

        public async Task<SenderEmailDto> UpdateAsync(Guid id, CreateUpdateSenderEmailDto input)
        {
            var items = await _senderEmailRepository.FindAsync(id);
            items.Email = input.Email;
            items.Password = input.Password;
            await _senderEmailRepository.UpdateAsync(items);
            return ObjectMapper.Map<SenderEmail, SenderEmailDto>(items);
        }
    }
}
