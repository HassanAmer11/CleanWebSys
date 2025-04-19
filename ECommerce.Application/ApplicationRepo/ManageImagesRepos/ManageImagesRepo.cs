using ECommerce.Core.Entities.Model;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.Repositories.BasRepository;
using LinqKit;
using System.Data.Entity;
using System.Linq.Expressions;

namespace ECommerce.Application.ApplicationRepo.ManageImagesRepos;

public class ManageImagesRepo : RepositoryApp<Image>, IManageImagesRepo
{

    public ManageImagesRepo(ApplicationDbContext context) : base(context)
    {

    }

    public async Task<List<Image>> GetImagesForProductAsync(Expression<Func<Image, bool>> firstCondition, Expression<Func<Image, bool>> secondCondition)
    {
        var combinedCondition = PredicateBuilder.New<Image>(false).And(firstCondition).And(secondCondition);
        return await Entity.AsNoTracking().Where(combinedCondition).ToListAsync();
    }
}
