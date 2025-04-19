using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
namespace ECommerce.Application.UploadImages;

public static class UploadImage
{
    public static List<string> _UploadMultipleImage(this IWebHostEnvironment webHost, List<IFormFile> imageList, string pathName)
    {
        var result = new List<string>();
        string src = "";
        string root = "wwwroot/";
        if (!Directory.Exists(root + $"ImageSrc/{pathName}/"))
        {
            Directory.CreateDirectory(root + $"ImageSrc/{pathName}/");
        }
        if (imageList.Count > 0)
        {
            foreach (IFormFile photo in imageList)
            {
                if (photo != null)
                {
                    src = $"ImageSrc/{pathName}/{Guid.NewGuid()}-{photo.FileName}";
                    string path = Path.Combine(webHost.ContentRootPath, root, src);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                    result.Add(src);
                }
            }

        }
        return result;

    }
    public static string _UploadImage(this IWebHostEnvironment webHost, IFormFile image, string pathName)
    {
        string src = "";
        string root = "wwwroot/";
        if (!Directory.Exists(root + $"ImageSrc/{pathName}/"))
        {
            Directory.CreateDirectory(root + $"ImageSrc/{pathName}/");
        }
        if (image != null)
        {
            src = $"ImageSrc/{pathName}/{Guid.NewGuid()}-{image.FileName}";
            string path = Path.Combine(webHost.ContentRootPath, root, src);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
        }
        return src;
    }

    public static void _RemoveImage(this IWebHostEnvironment WebHost, string OldFileName, string Paht)
    {
        if (!string.IsNullOrEmpty(Paht) && !string.IsNullOrEmpty(OldFileName))
        {
            string root = "wwwroot/";
            string oldfileName = OldFileName;
            var OldfullPath = Path.Combine(WebHost.ContentRootPath, root, oldfileName);
            System.IO.File.Delete(OldfullPath);
        }
    }


    public static string _UploadFile(this IWebHostEnvironment webHost, IFormFile file, string pathName)
    {
        string src = "";
        string root = "wwwroot/";
        if (!Directory.Exists(root + $"files/{pathName}/"))
        {
            Directory.CreateDirectory(root + $"files/{pathName}/");
        }
        if (file != null)
        {
            src = $"files/{pathName}/{Guid.NewGuid()}-{file.FileName}";
            string path = Path.Combine(webHost.ContentRootPath, root, src);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
        }
        return src;
    }
    public static void _RemoveFile(this IWebHostEnvironment WebHost, string OldFileName, string Paht)
    {
        if (!string.IsNullOrEmpty(Paht) && !string.IsNullOrEmpty(OldFileName))
        {
            string root = "wwwroot/";
            string oldfileName = OldFileName;
            var OldfullPath = Path.Combine(WebHost.ContentRootPath, root, oldfileName);
            System.IO.File.Delete(OldfullPath);
        }
    }

}
