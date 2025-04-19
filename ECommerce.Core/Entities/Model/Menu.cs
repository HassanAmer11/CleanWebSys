using ECommerce.Core.Entities.BaseModel;


namespace ECommerce.Core.Entities.Model
{
    public class Menu : MainBaseEntity
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public bool? IsActive { get; set; }

    }
}

