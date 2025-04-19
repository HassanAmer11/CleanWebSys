using ECommerce.Application.ApplicationRepo.CategoryRepos;
using ECommerce.Application.ApplicationRepo.ContactInfoRepos;
using ECommerce.Application.ApplicationRepo.ContentRepos;
using ECommerce.Application.ApplicationRepo.GovernorateRepos;
using ECommerce.Application.ApplicationRepo.ManageImagesRepos;
using ECommerce.Application.ApplicationRepo.MenuRepos;
using ECommerce.Application.ApplicationRepo.OrdersRepos;
using ECommerce.Application.ApplicationRepo.ProductRepos;
using ECommerce.Application.IUOW;
using ECommerce.Infrastructure.Context;

namespace ECommerce.Application.UOW;

public class UnitOfWork : IUnitOfWork
{
    #region Fields
    private readonly ApplicationDbContext _context;
    IGovernorateRepo _governorateRepository { get; }
    ICategoryRepo _categoryRepository { get; }
    IProductRepo _productRepository { get; }
    IManageImagesRepo _imagesRepository { get; }
    IOrderRepo _orderRepoRepository { get; }
    IMenuRepo _menuRepoRepository { get; }
    IContentRepo _contentRepoRepository { get; }
    IContactInfoRepo _contactInfoRepository { get; }
    #endregion

    #region Constractor
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
    #endregion

    #region Repo Getters
    public IProductRepo ProductRepo => _productRepository ?? new ProductRepo(_context);
    public IGovernorateRepo GovernorateRepo => _governorateRepository ?? new GovernorateRepo(_context);
    public ICategoryRepo CategoryRepo => _categoryRepository ?? new CategoryRepo(_context);
    public IManageImagesRepo ImagesRepo => _imagesRepository ?? new ManageImagesRepo(_context);
    public IOrderRepo OrderRepo => _orderRepoRepository ?? new OrderRepo(_context);
    public IContentRepo ContentRepo => _contentRepoRepository ?? new ContentRepo(_context);
    public IMenuRepo MenuRepo => _menuRepoRepository ?? new MenuRepo(_context);
    public IContactInfoRepo contactInfoRepo => _contactInfoRepository ?? new ContactInfoRepo(_context);
    #endregion

    #region UnitOfWork Methods
    public async Task<int> SaveCompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
        _context.Dispose();
    }
    #endregion
}
