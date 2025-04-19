using ECommerce.Core.Entities.BaseModel;

namespace ECommerce.Core.Entities.Model
{
    public class ContactInfo : MainBaseEntity
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