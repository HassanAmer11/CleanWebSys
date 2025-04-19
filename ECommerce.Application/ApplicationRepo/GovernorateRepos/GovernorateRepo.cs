using ECommerce.Core.Entities.Model;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.Repositories.BasRepository;

namespace ECommerce.Application.ApplicationRepo.GovernorateRepos;

public class GovernorateRepo : RepositoryApp<Governorate>, IGovernorateRepo
{
    public GovernorateRepo(ApplicationDbContext context) : base(context)
    { }
}
