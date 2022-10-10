using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyClothesCA.Application.Common.Interfaces;
using MyClothesCA.Infrastructure.Persistence;
using Web.Services;

namespace Web;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services/*,IConfiguration configuration*/)
    {

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        //todo:
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddMediatR(typeof(Program));

        services.Configure<CookiePolicyOptions>(
          options =>
          {
              options.CheckConsentNeeded = context => true;
              options.MinimumSameSitePolicy = SameSiteMode.None;
          });


        services.AddControllersWithViews(
             options =>
             {
                 options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
             });

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddAntiforgery(options =>
        {
            options.HeaderName = "X-CSRF-TOKEN";
        });


        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();


        services.AddRazorPages();

        return services;
    }
}
