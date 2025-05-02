using ECommerce.Core.Entities.Model;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.Repositories.BasRepository;

namespace ECommerce.Application.ApplicationRepo.ProductLocationRepos
{
    public class ProductLocationRepo : RepositoryApp<ProductLocation>, IProductLocationRepo
    {
        public ProductLocationRepo(ApplicationDbContext context) : base(context) { }
    }
}
