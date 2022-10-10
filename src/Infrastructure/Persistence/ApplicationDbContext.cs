
namespace MyClothesCA.Infrastructure.Persistence;

using System.Reflection;
using MyClothesCA.Application.Common.Interfaces;
using MyClothesCA.Domain.Entities;
using MyClothesCA.Infrastructure.Identity;
using MyClothesCA.Infrastructure.Persistence.Interceptors;
//using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using Microsoft.AspNetCore.Identity;
using MyClothesCA.Domain.Common;
using System.Security.Claims;
using MyClothesCA.Infrastructure.Common;

public class ApplicationDbContext : /*ApiAuthorizationDbContext*/ IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    //private readonly UserManager<ApplicationUser> userManager;


    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        /*IOptions<OperationalStoreOptions> operationalStoreOptions,*/
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor/*, UserManager<ApplicationUser> userManager*/)
        : base(options/*, operationalStoreOptions*/)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        //this.userManager = userManager;

    }
    public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Colour> Colours => Set<Colour>();
    public DbSet<Season> Seasons => Set<Season>();
    public DbSet<Garment> Clothes => Set<Garment>();
    public DbSet<Image> Images => Set<Image>();
    public DbSet<UserClothes> UserClothes => Set<UserClothes>();
    public DbSet<UserCategory> UsersCategories => Set<UserCategory>();

    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.Entity<UserClothes>(usergarment =>
        {
            usergarment.HasKey(ug => new { ug.ApplicationUserId, ug.GarmentId });

        });
        builder.Entity<UserCategory>(userCategory =>
        {
            userCategory.HasKey(uc => new { uc.ApplicationUserId, uc.CategoryId });

        });

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);

        this.ConfigureUserIdentityRelations(builder);

        builder.Seed();

    }
    private void ConfigureUserIdentityRelations(ModelBuilder builder) => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);

    }


    //Todo: add

    //private async Task DispatchEvents(DomainEvent[] events)
    //{
    //    foreach (var @event in events)
    //    {
    //        @event.IsPublished = true;
    //        await _domainEventService.Publish(@event);
    //    }
    //}

}

