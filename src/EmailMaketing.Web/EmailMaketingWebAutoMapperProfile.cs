using AutoMapper;
using EmailMaketing.Customers;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.Identity;

namespace EmailMaketing.Web;

public class EmailMaketingWebAutoMapperProfile : Profile
{
    public EmailMaketingWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        CreateMap<Pages.Customers.CreateModalModel.CreateCustomerViewModal, CreateUpdateCustomer>();
    }
}
