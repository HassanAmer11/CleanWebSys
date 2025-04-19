using ECommerce.Core.Entities.Model;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.Repositories.BasRepository;

namespace ECommerce.Application.ApplicationRepo.OrdersRepos;

public class OrderRepo : RepositoryApp<Order>, IOrderRepo
{
    public OrderRepo(ApplicationDbContext context) : base(context)
    {
    }
}
