using AutoMapper;
using EmailMaketing.SenderEmails;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.Identity;
using static EmailMaketing.Web.Pages.SenderEmails.CreateModalModel;

namespace EmailMaketing.Web;

public class EmailMaketingWebAutoMapperProfile : Profile
{
    public EmailMaketingWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        CreateMap<CreateSenderEmailViewModal, CreateUpdateSenderEmailDto>();
    }
}
