using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities.BaseModel
{
    public abstract class MainBaseEntity : BaseEntity
    {
        public DateTime Deleted { get; set; }
        public string DeletedBy { get; set; }
    }
}
