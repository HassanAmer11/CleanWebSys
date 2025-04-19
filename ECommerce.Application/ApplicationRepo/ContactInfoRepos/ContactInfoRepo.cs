using ECommerce.Core.Entities.Model;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.Repositories.BasRepository;

namespace ECommerce.Application.ApplicationRepo.ContactInfoRepos
{
    public class ContactInfoRepo : RepositoryApp<ContactInfo>, IContactInfoRepo
    {
        public ContactInfoRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
