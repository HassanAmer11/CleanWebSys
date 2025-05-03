using ECommerce.Core.Entities.BaseModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Core.Entities.Model
{
    public class ProductLocation: MainBaseEntity
    {
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [ForeignKey("GovernorateId")]
        public int GovernorateId { get; set; }
        public Governorate Governorate { get; set; }
    }
}
