using ECommerce.Application;
using ECommerce.Core;
using ECommerce.Core.Entities;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Add services to the container
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyOrigin()  // Allow all origins (all domains and ports)
                  .AllowAnyHeader()  // Allow any headers
                  .AllowAnyMethod(); // Allow any HTTP methods (GET, POST, etc.)
        });
    });

    #region Connect To SQL Server
    //var connectionString = builder.Configuration.GetConnectionString(name: "DefaultConnection");
    //builder.Services.AddDbContext<ApplicationDbContext>(opions => opions.UseSqlServer(connectionString));

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
       .AddEntityFrameworkStores<ApplicationDbContext>()
       .AddDefaultTokenProviders();

    #endregion

    #region Extension Methods
    builder.Services.AddApplicationDependencies(builder.Configuration);
    builder.Services.AddInfrastuctureDependencies();
    builder.Services.AddCoreDependencies();

    #endregion
    #region Configure Languages
    builder.Services.AddLocalization(opt =>
    {
        opt.ResourcesPath = "";
    });

    builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
        List<CultureInfo> supportedCultures = new List<CultureInfo>
            {
            new CultureInfo("en-US"),
            new CultureInfo("fr-FR"),
            new CultureInfo("ar-EG")
            };

        options.DefaultRequestCulture = new RequestCulture("en-US");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
    });
    #endregion
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    #region Localization Middleware
    var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
    app.UseRequestLocalization(options.Value);
    #endregion
    app.CustomStaticFiles(builder.Configuration);
    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();
    app.UseCors("AllowAll");
    app.MapControllers();

    app.Run();
}
catch (Exception e)
{
    Console.WriteLine(e);
}
