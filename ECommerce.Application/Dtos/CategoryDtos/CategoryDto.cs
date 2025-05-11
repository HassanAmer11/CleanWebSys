using ECommerce.Core.Entities.BaseModel;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Dtos.CategoryDtos
{

    public class CategoryBaseDto : BaseId
    {
        public string NameAr { get; set; }
        public string DescriptionAr { get; set; }
        public bool? ShowNavBar { get; set; }
    }
    public class CategoryGetDto : CategoryBaseDto
    {
        public string ImagePath { get; set; }
        public string IconPath { get; set; }
    }
    public class CategoryEditDto : CategoryBaseDto
    {
        public IFormFile Imagefile { get; set; }
        public IFormFile Iconfile { get; set; }
    }
    public class NavCategoryDto 
    {
        public int CategoryId { get; set; }
        public bool? ShowNavBar { get; set; }
    }
}
