using ECommerce.Core.Entities.Model;
using ECommerce.Core.IRepositories.IBasRepository;
using System.Linq.Expressions;

namespace ECommerce.Application.ApplicationRepo.ManageImagesRepos;

public interface IManageImagesRepo : IRepositoryApp<Image>
{
    public Task<List<Image>> GetImagesForProductAsync(Expression<Func<Image, bool>> firstCondition, Expression<Func<Image, bool>> secondCondition);
}
