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
using Volo.Abp.Users;

namespace EmailMaketing.SenderEmails
{
    //[Authorize(EmailMaketingPermissions.SenderEmails.Default)]
    public class SenderEmailAppService : ApplicationService, ISenderEmailAppService
    {
        private readonly ISenderEmailRepository _senderEmailRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IIdentityUserRepository _identityUserRepository;
        private readonly ICurrentUser _currentUser;

        public SenderEmailAppService(
            ISenderEmailRepository senderEmailRepository,
            ICustomerRepository customerRepository,
            IIdentityUserRepository identityUserRepository,
            ICurrentUser currentUser)
        {
            _senderEmailRepository = senderEmailRepository;
            _customerRepository = customerRepository;
            _identityUserRepository = identityUserRepository;
            _currentUser = currentUser;
        }

        public async Task<SenderEmailDto> CreateAsync(CreateUpdateSenderEmailDto input)
        {
            var SenderEmail = ObjectMapper.Map<CreateUpdateSenderEmailDto, SenderEmail>(input);
            await _senderEmailRepository.InsertAsync(SenderEmail);
            return ObjectMapper.Map<SenderEmail, SenderEmailDto>(SenderEmail);
        }

        public async Task<List<SenderEmailDto>> CreateManyAsync(List<CreateUpdateSenderEmailDto> senders)
        {
            var SenderEmails = ObjectMapper.Map<List<CreateUpdateSenderEmailDto>, List<SenderEmail>>(senders);
            await _senderEmailRepository.InsertManyAsync(SenderEmails);
            var senderEmailDeletes = await _senderEmailRepository.GetListAsync();
            return ObjectMapper.Map<List<SenderEmail>, List<SenderEmailDto>>(SenderEmails);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var sender = await _senderEmailRepository.FindAsync(id);
            var userIdAdmin = _currentUser.Id;
            var userAdmin = await _identityUserRepository.FindAsync((Guid)userIdAdmin);
            if (userAdmin.UserName == "admin")
            {
                await _senderEmailRepository.DeleteAsync(sender);
                return true;
            }
            if (sender.CustomerID != null)
            {
                var userId = _currentUser.Id;
                var customer = await _customerRepository.FindAsync(x => x.UserID == userId);
                if (sender.CustomerID == customer.Id)
                {
                    await _senderEmailRepository.DeleteAsync(sender);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<List<SenderEmailDto>> GetListSenderAsync()
        {
            var sender = await _senderEmailRepository.GetListAsync();
            return ObjectMapper.Map<List<SenderEmail>, List<SenderEmailDto>>(sender);
        }

        public async Task<bool> CheckEmailExist(string email)
        {
            var emailExist = await _senderEmailRepository.FindByEmailAsync(email);
            if (emailExist != null)
            {
                return true;
            }
            return false;
        }

        //public async Task<PagedResultDto<SenderEmailDto>> GetListAsync(GetSenderEmailInput input)
        //{
        //    //Set a default sorting, if not provided
        //    if (input.Sorting.IsNullOrWhiteSpace())
        //    {
        //        input.Sorting = nameof(SenderEmail.Email);
        //    }


        //    var senderemail = await _senderEmailRepository.GetListAsync(
        //        input.SkipCount,
        //        input.MaxResultCount,
        //        input.Sorting,
        //        input.Filter);
        //    //Convert to DTOs
        //    var senderEmailDtos = ObjectMapper.Map<List<SenderEmail>, List<SenderEmailDto>>(senderemail);
        //    //Get the total count with another query (required for the paging)
        //    var totalcount = await _senderEmailRepository.GetCountAsync();
        //    return new PagedResultDto<SenderEmailDto>
        //    {
        //        TotalCount = totalcount,
        //        Items = senderEmailDtos
        //    };
        //}

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
            var stt = 1;
            foreach(var item in senderWithNavigationDtos)
            {
                item.Stt = stt;
                stt++;
            }
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

        //get sender with IsSend = fale
        public async Task<SenderEmailDto> SenderIsSendFalseAsync()
        {
            var senders = await _senderEmailRepository.GetListAsync();
            foreach (var sender in senders)
            {
                if (sender.IsSend == false)
                {
                    sender.IsSend = true;
                    await _senderEmailRepository.UpdateAsync(sender);
                    var senderdto = ObjectMapper.Map<SenderEmail, SenderEmailDto>(sender);
                    return senderdto;
                }
            }
            return null;
        }

        //change all sender with IsSend = true to IsSend = false
        public async Task<bool> ChangeIsSendToFalseAsync()
        {
            var senders = await _senderEmailRepository.GetListAsync();
            foreach (var sender in senders)
            {
                sender.IsSend = false;
                await _senderEmailRepository.UpdateAsync(sender);
            }
            return true;
        }
    }
}
