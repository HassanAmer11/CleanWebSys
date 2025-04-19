using ECommerce.Core.Entities.BaseModel;
namespace ECommerce.Application.Dtos.MenuDtos;

public class MenuDto : BaseId
{
    public string Title { get; set; }
    public string Link { get; set; }
    public bool? IsActive { get; set; }
}