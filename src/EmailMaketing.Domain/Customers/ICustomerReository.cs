using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EmailMaketing.Customers
{
    public interface ICustomerReository : IRepository<Customer, Guid>
    {
        Task<List<Customer>> GetListAysnc(
                int SkipCount,
                int maxResultCount,
                string sorting,
                string filter
            );
    }
}
