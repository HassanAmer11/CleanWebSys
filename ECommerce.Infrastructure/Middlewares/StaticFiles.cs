using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace ECommerce.Infrastructure.Middlewares;

public static class StaticFiles
{
    public static IApplicationBuilder CustomStaticFiles(this IApplicationBuilder app, IConfiguration conf)
    {
        app.UseStaticFiles();

        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

        var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), conf.GetSection("Paths:PhysicalECommercePath").Value);
        if (!Directory.Exists(imagesPath))
            Directory.CreateDirectory(imagesPath);
        app.UseStaticFiles(
            new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(imagesPath),
                RequestPath = new PathString(conf.GetSection("Paths:ServerECommercePath").Value)
            });

        return app;
    }
}
