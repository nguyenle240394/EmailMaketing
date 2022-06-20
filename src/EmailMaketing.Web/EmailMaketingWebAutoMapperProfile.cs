using AutoMapper;
using EmailMaketing.Customers;
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
        CreateMap<Pages.Customers.CreateModalModel.CreateCustomerViewModal, CreateUpdateCustomer>();
        CreateMap<CustomerDto, Pages.Customers.EditModalModel.EditCustomerViewModal>();
        CreateMap<Pages.Customers.EditModalModel.EditCustomerViewModal, CreateUpdateCustomer>();

        CreateMap<CreateSenderEmailViewModal, CreateUpdateSenderEmailDto>();
    }
}
