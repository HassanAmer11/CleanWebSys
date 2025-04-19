using ECommerce.Core.Entities.BaseModel;

using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Core.Entities.Model
{
    public class Product : MainBaseEntity
    {
        public string NameAr { get; set; }
        public string DescAr { get; set; }
        public string DetailAr { get; set; }
        public string VideoUrl { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public bool? IsOffer { get; set; }
        public bool? ShowHome { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Image> Images { get; set; }

    }
}
