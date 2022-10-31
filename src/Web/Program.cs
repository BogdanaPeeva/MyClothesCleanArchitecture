
using Microsoft.EntityFrameworkCore;
using MyClothesCA.Infrastructure.Persistence;
using MyClothesCA.Infrastructure.Migrations;
using MyClothesCA.Application;
using Web;
using MyClothesCA.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();


// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebUIServices();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();

    // Initialise and seed database
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        //await dbContext.Database.MigrateAsync();

        //var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        //await initialiser.InitialiseAsync();
        //await initialiser.SeedAsync();
    }
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseWebSockets();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
               "ClothesCategory",
               "c/{name:minlength(3)}",
               new { controller = "Categories", action = "ByName" });
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.MapRazorPages();

// app.MapFallbackToFile("index.html");


app.Run();
