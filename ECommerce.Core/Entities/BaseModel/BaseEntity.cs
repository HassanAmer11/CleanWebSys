namespace ECommerce.Core.Entities.BaseModel
{
    public abstract class BaseEntity : BaseId
    {

        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }


    }
}
