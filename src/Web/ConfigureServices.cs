using System.Configuration;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MyClothesCA.Application.Common.Interfaces;
using MyClothesCA.Infrastructure.Persistence;
using Web.Services;

namespace Web;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        //todo:
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddMediatR(typeof(Program));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
           .AddCookie()
           .AddOpenIdConnect("Auth0", options =>
           {
               options.Authority = $"https://{configuration["Auth0:Domain"]}";

               options.ClientId = configuration["Auth0:ClientId"];
               options.ClientSecret = configuration["Auth0:ClientSecret"];

               options.ResponseType = OpenIdConnectResponseType.Code;

               options.Scope.Clear();
               options.Scope.Add("openid");

               options.CallbackPath = new PathString("/callback");

               options.ClaimsIssuer = "Auth0";

               options.Events = new OpenIdConnectEvents
               {
                   OnRedirectToIdentityProviderForSignOut = (context) =>
                   {
                       var logoutUri = $"https://{configuration["Auth0:Domain"]}/v2/logout?client_id={configuration["Auth0:Domain"]}";

                       var postLogoutUri = context.Properties.RedirectUri;
                       if (!string.IsNullOrEmpty(postLogoutUri))
                       {
                           if (postLogoutUri.StartsWith("/"))
                           {
                               var request = context.Request;
                               postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
                           }
                           logoutUri += $"&returnTo={Uri.EscapeDataString(postLogoutUri)}";
                       }
                       context.Response.Redirect(logoutUri);
                       context.HandleResponse();

                       return Task.CompletedTask;
                   }
               };

           });

        //services.Configure<CookiePolicyOptions>(
        //  options =>
        //  {
        //      options.CheckConsentNeeded = context => true;
        //      options.MinimumSameSitePolicy = SameSiteMode.None;
        //  });


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
