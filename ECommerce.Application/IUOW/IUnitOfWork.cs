using ECommerce.Application.ApplicationRepo.CategoryRepos;
using ECommerce.Application.ApplicationRepo.ContactInfoRepos;
using ECommerce.Application.ApplicationRepo.ContentRepos;
using ECommerce.Application.ApplicationRepo.GovernorateRepos;
using ECommerce.Application.ApplicationRepo.ManageImagesRepos;
using ECommerce.Application.ApplicationRepo.MenuRepos;
using ECommerce.Application.ApplicationRepo.OrdersRepos;
using ECommerce.Application.ApplicationRepo.ProductRepos;

namespace ECommerce.Application.IUOW;

public interface IUnitOfWork : IDisposable
{
    IGovernorateRepo GovernorateRepo { get; }
    IProductRepo ProductRepo { get; }
    IManageImagesRepo ImagesRepo { get; }
    ICategoryRepo CategoryRepo { get; }
    IOrderRepo OrderRepo { get; }
    IContentRepo ContentRepo { get; }
    IMenuRepo MenuRepo { get; }
    IContactInfoRepo contactInfoRepo { get; }
    Task<int> SaveCompleteAsync();
}