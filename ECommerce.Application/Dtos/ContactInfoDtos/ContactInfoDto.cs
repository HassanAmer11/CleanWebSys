using ECommerce.Core.Entities.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Dtos.ContactInfoDtos
{
    public class ContactInfoDto : BaseId
    {
        public string Phone { get; set; }
        public string WhatsApp { get; set; }
        public string FaceBookPage { get; set; }
        public string AboutUs { get; set; }
        public string Details { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string InstagramPage { get; set; }
        public string XPage { get; set; }
    }
}
