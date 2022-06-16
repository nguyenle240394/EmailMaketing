﻿using AutoMapper;
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
        CreateMap<CreateUpdateCustomer, Customer>();
        CreateMap<Customer, CustomerDto>();

        CreateMap<SenderEmail, SenderEmailDto>();
        CreateMap<CreateUpdateSenderEmailDto, SenderEmail>();
    }
}
