using ECommerce.Core.Entities.BaseModel;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Dtos.CategoryDtos
{

    public class CategoryBaseDto : BaseId
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
    }
    public class CategoryGetDto : CategoryBaseDto
    {
        public string ImagePath { get; set; }
    }
    public class CategoryEditDto : CategoryBaseDto
    {
        public IFormFile file { get; set; }
    }
}
