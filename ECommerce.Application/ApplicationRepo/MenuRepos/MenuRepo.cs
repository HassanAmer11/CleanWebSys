using ECommerce.Core.Entities.Model;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.Repositories.BasRepository;

namespace ECommerce.Application.ApplicationRepo.MenuRepos
{
    public class MenuRepo : RepositoryApp<Menu>, IMenuRepo
    {
        public MenuRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
