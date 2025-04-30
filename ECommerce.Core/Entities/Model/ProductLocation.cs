using ECommerce.Core.Entities.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities.Model
{
    public class ProductLocation: MainBaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int GovernorateId { get; set; }
        public Governorate Governorate { get; set; }
    }
}
