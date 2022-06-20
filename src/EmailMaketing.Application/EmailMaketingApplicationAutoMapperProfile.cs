using AutoMapper;
using EmailMaketing.ContentEmails;

namespace EmailMaketing;

public class EmailMaketingApplicationAutoMapperProfile : Profile
{
    public EmailMaketingApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<ContentEmail, ContentEmailDto>();
        CreateMap<CreateUpdateContentEmailDto, ContentEmail>();
    }
}
