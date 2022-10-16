using MyClothesCA.Application.Common.Interfaces;
using MyClothesCA.Infrastructure.Persistence;
using MyClothesCA.Infrastructure.Persistence.Interceptors;
using MyClothesCA.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyClothesCA.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MyClothesCA.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("MyClothesCADb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<ApplicationDbContext>();

            //services.AddScoped<ApplicationDbContextInitialiser>();


            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                    /*.AddRoles<ApplicationRole>()*/
                    .AddEntityFrameworkStores<ApplicationDbContext>();



            services.AddTransient<IDateTime, DateTimeService>();


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddAuthorization(options =>
            //    options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));


            return services;
        
    }
}
