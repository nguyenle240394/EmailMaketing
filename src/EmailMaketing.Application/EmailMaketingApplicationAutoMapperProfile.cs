﻿using AutoMapper;
using EmailMaketing.ContentEmails;
using EmailMaketing.Customers;
using EmailMaketing.SenderEmails;

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

        CreateMap<CreateUpdateCustomer, Customer>();
        CreateMap<Customer, CustomerDto>();
        CreateMap<Customer, CustomerLookupDto>();

        CreateMap<SenderEmail, SenderEmailDto>();
        CreateMap<SenderWithNavigation, SenderWithNavigationDto>();
        CreateMap<CreateUpdateSenderEmailDto, SenderEmail>();
    }
}
