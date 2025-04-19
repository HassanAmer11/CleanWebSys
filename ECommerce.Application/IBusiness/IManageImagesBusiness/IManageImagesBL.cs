using ECommerce.Core.Common.Response;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.IBusiness.IManageImagesBusiness
{
    public interface IManageImagesBL
    {
        Task<ResponseApp<int>> UploadImageAsync(IFormFile file, int entityId);
        Task<ResponseApp<List<int>>> UploadImagesAsync(List<IFormFile> files, int entityId);
        Task<ResponseApp<int>> UploadImageAsync(IFormFile file, int entityId, string pathName);
        Task<ResponseApp<List<int>>> UploadImagesAsync(List<IFormFile> files, int entityId, string pathName);
        Task<ResponseApp<int>> UpdateImageAsync(IFormFile fileNew, int fileIdOLd, int entityId);
        Task<string> GetImagePathAsync(IFormFile file);
        string GetImagePath(IFormFile file, string pathName);
        Task<ResponseApp<string>> DeleteImage(int id);
    }
}
