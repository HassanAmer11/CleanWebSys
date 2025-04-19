using ECommerce.Application.ApplicationRepo.CategoryRepos;
using ECommerce.Application.ApplicationRepo.ContactInfoRepos;
using ECommerce.Application.ApplicationRepo.ContentRepos;
using ECommerce.Application.ApplicationRepo.ManageImagesRepos;
using ECommerce.Application.ApplicationRepo.MenuRepos;
using ECommerce.Application.ApplicationRepo.OrdersRepos;
using ECommerce.Application.ApplicationRepo.ProductRepos;
using ECommerce.Application.Business.AuthBusiness;
using ECommerce.Application.Business.CategoryBusiness;
using ECommerce.Application.Business.ContactInfoBusiness;
using ECommerce.Application.Business.ContentBusiness;
using ECommerce.Application.Business.GovernoratesBL;
using ECommerce.Application.Business.ManageImagesBusiness;
using ECommerce.Application.Business.MenuBusiness;
using ECommerce.Application.Business.OrderBusiness;
using ECommerce.Application.Business.ProductBusiness;
using ECommerce.Application.Helpers;
using ECommerce.Application.IBusiness.IAuthBusiness;
using ECommerce.Application.IBusiness.ICategoryBusiness;
using ECommerce.Application.IBusiness.IContactInfoBusiness;
using ECommerce.Application.IBusiness.IContentBusiness;
using ECommerce.Application.IBusiness.IGovernorateBusiness;
using ECommerce.Application.IBusiness.IManageImagesBusiness;
using ECommerce.Application.IBusiness.IMenuBusiness;
using ECommerce.Application.IBusiness.IOrderBusiness;
using ECommerce.Application.IBusiness.IProductBusiness;
using ECommerce.Application.IUOW;
using ECommerce.Application.UOW;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;


namespace ECommerce.Application;

public static class ModuleApplicationDependencies
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        //Auto Mapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IProductRepo, ProductRepo>();
        services.AddScoped<IManageImagesRepo, ManageImagesRepo>();
        services.AddScoped<IOrderRepo, OrderRepo>();
        services.AddScoped<ICategoryRepo, CategoryRepo>();
        services.AddScoped<IMenuRepo, MenuRepo>();
        services.AddScoped<IContentRepo, ContentRepo>();
        services.AddScoped<IContactInfoRepo, ContactInfoRepo>();

        services.AddScoped<IManageImagesBL, ManageImagesBL>();
        services.AddScoped<IGovernoratesBL, GovernoratesBL>();
        services.AddScoped<IProductBL, ProductBL>();
        services.AddScoped<ICategoryBL, CategoryBL>();
        services.AddScoped<IOrderRepoBL, OrderRepoBL>();
        services.AddScoped<IContentBL, ContentBL>();
        services.AddScoped<IMenuBL, MenuBL>();
        services.AddScoped<IContactInfoBL, ContactInfoBL>();
        services.AddScoped<IAuth, Auth>();

        #region JWT Authentication Configure
        var jwtSection = configuration.GetSection("Jwt");

        var jwtOptions = jwtSection.Get<JwtOptions>();
        services.AddSingleton(jwtOptions);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
             .AddJwtBearer(options =>
             {
                 options.RequireHttpsMetadata = false;
                 options.SaveToken = true;
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = jwtOptions.Issuer,
                     ValidAudience = jwtOptions.Audience,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
                 };
             });
        #endregion

        return services;
    }

}
