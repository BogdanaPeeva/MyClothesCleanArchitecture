using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyClothesCA.Domain.Entities;

namespace MyClothesCA.Infrastructure.Persistence.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> appUser)
        {
            appUser
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            appUser
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // todo:
            //appUser
            //    .HasMany(e => e.Roles)
            //    .WithOne()
            //    .HasForeignKey(e => e.UserId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Restrict);

            appUser
            .HasMany(au => au.Clothes)
            .WithOne()
            .HasForeignKey(ur => ur.ApplicationUserId);

            appUser
            .HasMany(au => au.UserClothesCollection)
            .WithOne(u => u.ApplicationUser)
           .OnDelete(DeleteBehavior.Restrict);


        }

    }
}


