using ECommerce.Core.Entities.BaseModel;
using ECommerce.Core.Entities.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Application.Dtos.ProductDtos
{
    public class ProductBase : BaseId
    {
        public string NameAr { get; set; }
        public string DescAr { get; set; }
        public string DetailAr { get; set; }
        public int CategoryId { get; set; }
        public bool? ShowHome { get; set; }
        public string VideoUrl { get; set; }

    }
}
