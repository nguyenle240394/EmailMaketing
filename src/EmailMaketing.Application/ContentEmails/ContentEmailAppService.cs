using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EmailMaketing.ContentEmails
{
    public class ContentEmailAppService : ApplicationService, IContentEmailAppService
    {
        private readonly IContentEmailRepository _ContentEmailRepository;
        public ContentEmailAppService(IContentEmailRepository contentEmailRepository)
        {
            _ContentEmailRepository = contentEmailRepository;
        }
        public async Task<ContentEmailDto> CreateAsync(CreateUpdateContentEmailDto input)
        {
            var contentEmail = ObjectMapper.Map<CreateUpdateContentEmailDto, ContentEmail>(input);
            await _ContentEmailRepository.InsertAsync(contentEmail);
            return ObjectMapper.Map<ContentEmail,ContentEmailDto>(contentEmail);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var findid = await _ContentEmailRepository.FindAsync(id);
            if (findid == null) return false;
            else
            {
                await _ContentEmailRepository.DeleteAsync(id);
                return true;
            }
        }

        public async Task<ContentEmailDto> GetEmailAsync(Guid id)
        {
            var GetaEmail = await _ContentEmailRepository.GetAsync(id);
            return ObjectMapper.Map<ContentEmail, ContentEmailDto>(GetaEmail);
        }

        public async Task<List<ContentEmailDto>> GetListsEmailAsync(Guid id)
        {
            var ContentEmails = await _ContentEmailRepository.GetListAsync();
            var listContentEmails = ContentEmails.Where(x => x.CustomerID == id).ToList();
            var ContentEmaiDtos = ObjectMapper.Map<List<ContentEmail>, List<ContentEmailDto>>(listContentEmails);
            return ContentEmaiDtos;
        }

        public async Task<ContentEmailDto> UpdateDataAsync(Guid id, CreateUpdateContentEmailDto input)
        {
            var ContentEmails = await _ContentEmailRepository.FindAsync(id);
            ContentEmails.Subject = input.Subject;
            ContentEmails.Body = input.Body;
            ContentEmails.SenderEmail = input.SenderEmail;
            ContentEmails.Status = input.Status;
            ContentEmails.Attachment = input.Attachment;
            await _ContentEmailRepository.UpdateAsync(ContentEmails);
            return ObjectMapper.Map<ContentEmail, ContentEmailDto>(ContentEmails);
        }
    }
}
