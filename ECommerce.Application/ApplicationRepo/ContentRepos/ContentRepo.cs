using ECommerce.Core.Entities.Model;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.Repositories.BasRepository;


namespace ECommerce.Application.ApplicationRepo.ContentRepos
{
    public class ContentRepo : RepositoryApp<Content>, IContentRepo
    {
        public ContentRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
