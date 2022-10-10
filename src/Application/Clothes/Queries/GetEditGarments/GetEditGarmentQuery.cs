using System;
using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using MyClothesCA.Application.Common.Interfaces;
using MyClothesCA.Domain.Entities;
using MyClothesCA.Application.Clothes.Queries.GetClothes;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyClothesCA.Application.Categories.Queries.GetCategories;
using MyClothesCA.Application.DTOs.ClothesDTOs;

namespace MyClothesCA.Application.Clothes.Queries.GetEditGarments;

public class GetEditGarmentQuery : IRequest<EditGarmentDto>
{
    public GetEditGarmentQuery(string id)
    {
        this.Id = id;
    }
    public string Id { get; }
}
public class GetEditGarmentQueryHandler : IRequestHandler<GetEditGarmentQuery, EditGarmentDto>
{
    private readonly IApplicationDbContext dbContext;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IMapper mapper;
    private readonly IMediator mediator;
    private readonly IHttpContextAccessor httpContextAccessor;

    public GetEditGarmentQueryHandler(IApplicationDbContext dbContext, IMapper mapper, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IMediator mediator)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.mediator = mediator;
        this.userManager = userManager;
        this.httpContextAccessor = httpContextAccessor;

    }

    public async Task<EditGarmentDto> Handle(GetEditGarmentQuery request, CancellationToken cancellationToken)
    {
        var user = await this.userManager.GetUserAsync(httpContextAccessor.HttpContext?.User);

        var userId = await this.userManager.GetUserIdAsync(user);
        
        var query = new GetCategoriesByUserIdQuery();

        var getAllCategoriesByUserId = await this.mediator.Send(query, cancellationToken);

        async Task<SelectList> CategoryList()
        {
            var categories = await dbContext.UsersCategories
              .Where(x => x.ApplicationUserId == userId)
            .Select(x => x.Category).OrderBy(x => x.Name).ToListAsync(cancellationToken);

            //var categoriesSelectList = new SelectList(categories, "CategoryId", "Name");

            return new SelectList(categories, "CategoryId", "Name");
        }
        async Task<SelectList> SeasonList()
        {
            var seasons = await dbContext.Seasons.OrderBy(x => x.Name).ToListAsync(cancellationToken);
            //.Where(x => x.ApplicationUserId == userId)
            ////.Select(x => x.Seasons).OrderBy(x => x.Name).ToListAsync();

            //var seasonsSelectList = new SelectList(seasons, "SeasonId", "Name");

            return new SelectList(seasons, "SeasonId", "Name");
        }
        async Task<SelectList> ColourList()
        {
            var colours = await dbContext.Colours.OrderBy(x => x.Name).ToListAsync(cancellationToken);
            ////.Where(x => x.ApplicationUserId == userId)
            ////.Select(x => x.Colours).OrderBy(x => x.Name).ToListAsync();

            //var coloursSelectList = new SelectList(colours, "ColourId", "Name");

            return new SelectList(colours, "ColourId", "Name");
        }
        Garment garment = await this.dbContext.Clothes.FindAsync(request.Id);

        if (garment == null)
        {
            throw new Exception("Garment doesn't exist!");
        }

        //todo: meidiator.send here
        var editGarment = new EditGarmentDto()
        {
            GarmentId = garment.GarmentId,
            CategoryNames = await CategoryList(),
            SeasonNames = await SeasonList(),//seasonsSelectList,
            ColourNames = await ColourList(),//coloursSelectList,
        };

        //EditGarmentDto editGarmentModel = this.mapper.Map<EditGarmentDto>(garment);

        return editGarment;
    }
}

