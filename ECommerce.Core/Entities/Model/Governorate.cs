using ECommerce.Core.Entities.BaseModel;

namespace ECommerce.Core.Entities.Model
{
    public class Governorate : MainBaseEntity
    {
        public string NameAr { get; set; }
        public bool IsFreeDelivery { get; set; }
        public decimal DeliverdFees { get; set; }

    }
}
