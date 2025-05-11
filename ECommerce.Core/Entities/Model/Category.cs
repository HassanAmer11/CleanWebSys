using ECommerce.Core.Entities.BaseModel;

namespace ECommerce.Core.Entities.Model
{
    public class Category : MainBaseEntity
    {
        public string NameAr { get; set; }
        public string DescriptionAr { get; set; }
        public string ImagePath { get; set; }
        public string IconPath { get; set; }
        public bool? ShowNavBar { get; set; }
        public ICollection<Product> products { get; set; }


    }
}
