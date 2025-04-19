using ECommerce.Core.Entities.BaseModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Core.Entities.Model
{
    public class Image : MainBaseEntity
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public long Size { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product product { get; set; }
    }
}
