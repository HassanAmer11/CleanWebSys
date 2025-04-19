using AutoMapper;
using ECommerce.Application.Dtos.CategoryDtos;
using ECommerce.Application.Dtos.ContactInfoDtos;
using ECommerce.Application.Dtos.ContentDtos;
using ECommerce.Application.Dtos.GovernoratesDtos;
using ECommerce.Application.Dtos.ImagesDtos;
using ECommerce.Application.Dtos.MenuDtos;
using ECommerce.Application.Dtos.OrdersDtos;
using ECommerce.Application.Dtos.ProductDtos;
using ECommerce.Core.Entities.Model;

namespace ECommerce.Application.AutoMappers;

public class ApplicationMapperProfiles : Profile
{
    public ApplicationMapperProfiles()
    {
        #region Governorates
        CreateMap<GovernoratesDto, Governorate>().ReverseMap();
        #endregion

        #region CategoryDto

        CreateMap<Category, CategoryGetDto>();
        CreateMap<CategoryEditDto, Category>().ReverseMap();
        #endregion
        #region ProductDTO
        CreateMap<Product, ProductGetDto>();
        CreateMap<Image, ImagesDto>();
        CreateMap<Category, CategoryBaseDto>();
        CreateMap<ProductEditDto, Product>().ReverseMap();
        #endregion
        #region OrderDTO
        CreateMap<OrdersEditDto, Order>().ReverseMap();
        CreateMap<Order, OrdersGetDto>()
               .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
               .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.NameAr))
               .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
               .ForMember(dest => dest.GovernorateId, opt => opt.MapFrom(src => src.Governorates.Id))
               .ForMember(dest => dest.GovernorateName, opt => opt.MapFrom(src => src.Governorates.NameAr));
        #endregion
        #region Contents
        CreateMap<ContentDto, Content>().ReverseMap();
        #endregion
        #region Menus
        CreateMap<MenuDto, Menu>().ReverseMap();
        #endregion
        #region ContactInfo
        CreateMap<ContactInfoDto, ContactInfo>().ReverseMap();
        #endregion

    }
}
