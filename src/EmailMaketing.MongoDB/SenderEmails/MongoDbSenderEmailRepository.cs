using EmailMaketing.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace EmailMaketing.SenderEmails
{
    public class MongoDbSenderEmailRepository :
        MongoDbRepository<EmailMaketingMongoDbContext, SenderEmail, Guid>,
        ISenderEmailRepository
    {
        public MongoDbSenderEmailRepository(IMongoDbContextProvider<EmailMaketingMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        { }

        public async Task<List<SenderEmail>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetMongoQueryableAsync();
            return await query
                .WhereIf<SenderEmail, IMongoQueryable<SenderEmail>>(
                !filter.IsNullOrWhiteSpace(),
                senderemail => senderemail.Email.Contains(filter))
                .OrderByDescending(x => x.CreationTime)
                .As<IMongoQueryable<SenderEmail>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

        //public async Task<List<SenderEmail>> GetListSendEmailLookupAsync(
        //    Guid? Id, 
        //    string filterText = null, 
        //    string name = null, 
        //    string sorting = null, 
        //    int maxResultCount = int.MaxValue, 
        //    int skipCount = 0, 
        //    CancellationToken cancellationToken = default)
        //{
        //    var query = await GetMongoQueryableAsync();
        //    var result = await query.As<IMongoQueryable<SenderEmail>>()
        //        .OrderByDescending(x => x.CreationTime)
        //        .PageBy<SenderEmail, IMongoQueryable<SenderEmail>>(skipCount, maxResultCount)
        //        .ToListAsync(GetCancellationToken(cancellationToken));
        //    return result;
        //}
    }
}
