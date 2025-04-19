using ECommerce.Core.Entities.BaseModel;
namespace ECommerce.Application.Dtos.ContentDtos;

public class ContentDto : BaseId
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Details { get; set; }
}