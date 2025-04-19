using ECommerce.Core.Entities.BaseModel;

namespace ECommerce.Application.Dtos.ProductDtos
{
    public class ProductBase : BaseId
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescAr { get; set; }
        public string DescEn { get; set; }
        public string DetailAr { get; set; }
        public string DetailEn { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public int CategoryId { get; set; }
        public bool? IsOffer { get; set; }
        public bool? ShowHome { get; set; }
        public string VideoUrl { get; set; }

    }
}
