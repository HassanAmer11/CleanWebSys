using ECommerce.Core.Entities.Model;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.Repositories.BasRepository;

namespace ECommerce.Application.ApplicationRepo.ProductRepos;

public class ProductRepo : RepositoryApp<Product>, IProductRepo
{
    public ProductRepo(ApplicationDbContext context) : base(context)
    {

    }
}
