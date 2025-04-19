using ECommerce.Core.Entities.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities.Model
{
    public class Content : MainBaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
    }
}

