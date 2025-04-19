using AutoMapper;
using ECommerce.Application.IBusiness.IManageImagesBusiness;
using ECommerce.Application.IUOW;
using ECommerce.Application.UploadImages;
using ECommerce.Core.Common.Response;
using ECommerce.Core.Entities.Model;
using ECommerce.Core.Resources;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

namespace ECommerce.Application.Business.ManageImagesBusiness;

public class ManageImagesBL : ResponseHandler, IManageImagesBL
{
    #region Fields
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _iConfiguration;
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly IWebHostEnvironment _webHost;
    #endregion
    #region Constractor
    public ManageImagesBL(IMapper mapper, IUnitOfWork unitOfWork, IConfiguration iConfiguration, IWebHostEnvironment webHost, IStringLocalizer<SharedResources> localizer) : base(localizer)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _localizer = localizer;
        _iConfiguration = iConfiguration;
        _webHost = webHost;
    }
    #endregion
    #region Handle Functions
    public async Task<ResponseApp<int>> UploadImageAsync(IFormFile file, int entityId)
    {
        var id = await SaveImageAsync(file, entityId);
        return Success(id);

    }
    public async Task<ResponseApp<List<int>>> UploadImagesAsync(List<IFormFile> files, int entityId)
    {
        List<int> Ids = new List<int>();

        foreach (var file in files)
        {
            Ids.Add(await SaveImageAsync(file, entityId));

        }
        return Success(Ids);

    }
    public async Task<ResponseApp<int>> UploadImageAsync(IFormFile file, int entityId, string pathName)
    {
        var id = await SaveImageAsync(file, entityId, pathName);
        return Success(id);

    }
    public async Task<ResponseApp<List<int>>> UploadImagesAsync(List<IFormFile> files, int entityId, string pathName)
    {
        List<int> Ids = new List<int>();

        foreach (var file in files)
        {
            Ids.Add(await SaveImageAsync(file, entityId, pathName));

        }
        return Success(Ids);

    }

    public async Task<ResponseApp<int>> UpdateImageAsync(IFormFile fileNew, int fileIdOLd, int entityId)
    {
        var id = await UpdateFile(fileNew, fileIdOLd, entityId);
        return Success(id);

    }
    public async Task<ResponseApp<int>> UpdateImagesAsync(List<IFormFile> newImageIds, List<int> oldImageIds, int entityId)
    {
        // Ensure that the lists are of the same length
        if (newImageIds.Count != oldImageIds.Count)
        {
            return BadRequest<int>(_localizer[LanguageKey.NotFound]);
        }

        // List to store the results of each UpdateFile call
        var results = new List<int>();

        // Loop through the lists and call UpdateFile for each pair
        for (int i = 0; i < newImageIds.Count; i++)
        {
            var fileNew = newImageIds[i];
            var fileIdOld = oldImageIds[i];

            var id = await UpdateFile(fileNew, fileIdOld, entityId);
            results.Add(id);
        }

        // Assuming you want to return the last updated ID or some aggregated result
        return Success(results.LastOrDefault());
    }

    public async Task<ResponseApp<string>> DeleteImage(int id)
    {
        var image = await _unitOfWork.ImagesRepo.GetByIdAsync(id);
        if (image != null)
        {
            string path = _iConfiguration.GetSection("Paths:PhysicalECommercePath").Value;

            var uploadPath = Path.Combine(path, image.ImagePath);
            if (File.Exists(uploadPath))
                File.Delete(uploadPath);

            await _unitOfWork.ImagesRepo.DeleteAsync(image);
        }
        return Success<string>(_localizer[LanguageKey.DeletedSuccessfully]);
    }
    #endregion
    #region Base Image Functions
    private string GetPaths()
    {
        string path = _iConfiguration.GetSection("Paths:PhysicalECommercePath").Value;
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        return path;
    }
    public async Task<string> GetImagePathAsync(IFormFile file)
    {
        string path = GetPaths();
        string extension = Path.GetExtension(file.FileName);
        string fileServer = $"{Guid.NewGuid():N}{extension}";
        string filePath = Path.Combine(path, fileServer).ToLower();
        var fileStream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fileStream);
        await fileStream.DisposeAsync();
        string logicalPath = fileServer;
        return logicalPath;
    }
    private async Task<int> SaveImageAsync(IFormFile file, int entityId)
    {

        string delimiter = ".";
        int index = file.FileName.IndexOf(delimiter);
        string imageName = null;
        if (index != -1) imageName = file.FileName.Substring(0, index);

        Image image = new Image()
        {
            ImagePath = await GetImagePathAsync(file),
            Name = imageName,
            Size = file.Length,
            Created = DateTime.Now,
            ProductId = entityId

        };
        await _unitOfWork.ImagesRepo.AddAsync(image);
        return image.Id;
    }

    public string GetImagePath(IFormFile file, string pathName)
    {
        return UploadImage._UploadImage(_webHost, file, pathName);
    }
    private async Task<int> SaveImageAsync(IFormFile file, int entityId, string pathName)
    {
        string delimiter = ".";
        int index = file.FileName.IndexOf(delimiter);
        string imageName = null;
        if (index != -1) imageName = file.FileName.Substring(0, index);

        Image image = new Image()
        {
            ImagePath = GetImagePath(file, pathName),
            Name = imageName,
            Size = file.Length,
            Created = DateTime.Now,
            ProductId = entityId

        };
        await _unitOfWork.ImagesRepo.AddAsync(image);
        return image.Id;
    }

    private async Task<int> UpdateFile(IFormFile fileNew, int fileIdOLd, int entityId)
    {

        var image = _unitOfWork.ImagesRepo.GetById(fileIdOLd);
        if (image != null)
        {
            string directoryPath = _iConfiguration.GetSection("Paths:PhysicalECommercePath").Value;
            var uploadPath = Path.Combine(directoryPath, image.ImagePath);
            if (File.Exists(uploadPath))
                File.Delete(uploadPath);
        }
        string path = GetPaths();
        string extension = Path.GetExtension(fileNew.FileName);
        string fileServer = $"{Guid.NewGuid():N}{extension}";
        string filePath = Path.Combine(path, fileServer).ToLower();
        var fileStream = new FileStream(filePath, FileMode.Create);
        await fileNew.CopyToAsync(fileStream);
        await fileStream.DisposeAsync();
        var logicalPath = fileServer;

        string delimiter = ".";
        int index = fileNew.FileName.IndexOf(delimiter);
        string imageName = null;
        if (index != -1) imageName = fileNew.FileName.Substring(0, index);


        image.ImagePath = logicalPath;
        image.Name = imageName;
        image.ProductId = entityId;
        image.Size = fileNew.Length;


        await _unitOfWork.ImagesRepo.UpdateAsync(image);

        return image.Id;

    }
    #endregion
}
