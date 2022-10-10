using MyClothesCA.Domain.Entities;
using MyClothesCA.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MyClothesCA.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        //// Default roles
        //var administratorRole = new IdentityRole("Administrator");

        //if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        //{
        //    await _roleManager.CreateAsync(administratorRole);
        //}

        //// Default users
        //var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        //if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        //{
        //    await _userManager.CreateAsync(administrator, "Administrator1!");
        //    await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
        //}

        // Default data
        // Seed, if necessary
        if (!_context.Categories.Any())
        {
            _context.Categories.AddRange
                (
                 new Category()
                 {
                     CategoryId = 1.ToString(),
                     Name = "Tops",
                     ImageUrl = "https://images.unsplash.com/photo-1620799140408-edc6dcb6d633?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=772&q=80"
                 },

                new Category()
                {
                    CategoryId = 2.ToString(),
                    Name = "Bottoms",
                    ImageUrl = "https://image.made-in-china.com/202f0j00KstVjilPEIov/Spring-Autumn-Solid-Children-Sweatpants-Overalls-Casual-Kids-Clothes-Trousers-Pants-for-Toddler-Baby-Boys.jpg"
                },

                new Category()
                {
                    CategoryId = 3.ToString(),
                    Name = "Shoes",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRasPMutrSVbqShT-0oGXzDQzvW02zn1qxHZw&usqp=CAU"
                },

                new Category()
                {
                    CategoryId = 4.ToString(),
                    Name = "Dresses",
                    ImageUrl = "https://media.istockphoto.com/photos/fashion-in-new-york-city-picture-id186543936?k=20&m=186543936&s=612x612&w=0&h=gHHBtMHDGD7EnVRZawGxKpSI_LEhYEvS49DGo3UYu4I="
                },

                new Category()
                {
                    CategoryId = 5.ToString(),
                    Name = "Bags",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSDCbH7ePNeJPR_vH1LejSPlGtItvFEM4dc4g&usqp=CAU"
                },

                new Category()
                {
                    CategoryId = 6.ToString(),
                    Name = "Accessories",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR852-Y1089K1O-4Z3A5kcuYqEE2Yg9T_95Dg&usqp=CAU"
                },

                new Category()
                {
                    CategoryId = 7.ToString(),
                    Name = "Outerwear",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTK7lBmtYRlf3xGbkpz12obB6wk61zW4-LTX7I5XwdN2jBvSslJlCS9O1r_TUR77Fts4zQ&usqp=CAU"

                }
                );

        }

        if (!_context.Seasons.Any())
        {

            _context.Seasons.AddRange(

            new Season() { Name = "Autumn " },
            new Season() { Name = "Winter" },
            new Season() { Name = "Spring" },
            new Season() { Name = "Summer" }

                );
        }
        if (!_context.Colours.Any())
        {

            _context.Colours.AddRange(

            new Colour() { ColourId = 1.ToString(), Name = "White" },
            new Colour() { ColourId = 2.ToString(), Name = "Black" },
            new Colour() { ColourId = 3.ToString(), Name = "Red" },
            new Colour() { ColourId = 4.ToString(), Name = "Blue" },
            new Colour() { ColourId = 5.ToString(), Name = "Orange" },
            new Colour() { ColourId = 6.ToString(), Name = "Brown" },
            new Colour() { ColourId = 7.ToString(), Name = "Beige" },
            new Colour() { ColourId = 8.ToString(), Name = "Green" },
            new Colour() { ColourId = 9.ToString(), Name = "Yellow" },
            new Colour() { ColourId = 10.ToString(), Name = "Turquoise" },
            new Colour() { ColourId = 11.ToString(), Name = "Mint" },
            new Colour() { ColourId = 12.ToString(), Name = "Ruby" }
            );

        }

        await _context.SaveChangesAsync();
    }
}

