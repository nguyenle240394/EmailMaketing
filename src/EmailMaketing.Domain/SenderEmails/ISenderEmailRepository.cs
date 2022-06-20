using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EmailMaketing.SenderEmails
{
    public interface ISenderEmailRepository : IRepository<SenderEmail, Guid>
    {
        Task<List<SenderEmail>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null,
            CancellationToken cancellationToken = default
        );
        Task<List<SenderWithNavigation>> GetListWithNavigationAsync(
          int skipCount,
          int maxResultCount,
          string sorting,
          string filter = null,
          CancellationToken cancellationToken = default);

        //Task<List<SenderEmail>> GetListSendEmailLookupAsync(
        //    Guid? Id,
        //    string filterText = null,
        //    string name = null,
        //    string sorting = null,
        //    int maxResultCount = int.MaxValue,
        //    int skipCount = 0,
        //    CancellationToken cancellationToken = default);
    }
}
