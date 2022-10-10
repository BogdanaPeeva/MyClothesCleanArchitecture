using MyClothesCA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MyClothesCA.Application.Common.Interfaces;

public interface IApplicationDbContext
{

    DbSet<Category> Categories { get; }
    DbSet<Colour> Colours { get; }
    DbSet<Season> Seasons { get; }
    DbSet<Garment> Clothes { get; }
    DbSet<Image> Images { get; }
    DbSet<UserClothes> UserClothes { get; }
    DbSet<ApplicationUser> ApplicationUsers { get; }
    DbSet<UserCategory> UsersCategories { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    
}
