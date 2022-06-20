using EmailMaketing.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace EmailMaketing.ContentEmails
{
    public class MongoDbContentEmailRepository : MongoDbRepository<EmailMaketingMongoDbContext, ContentEmail, Guid>, IContentEmailRepository
    {
        public MongoDbContentEmailRepository(IMongoDbContextProvider<EmailMaketingMongoDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
    }
}
