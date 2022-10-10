using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using MyClothesCA.Application.Common.Interfaces;
using MyClothesCA.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyClothesCA.Application.Categories.Queries.GetCategories;
using MyClothesCA.Application.DTOs.ClothesDTOs;

namespace MyClothesCA.Application.Clothes.Queries.GetClothes;

public class GetSelectListQuery : IRequest<CreateGarmentInputDto>
{

}
public class GetAddSelectListQueryHandler : IRequestHandler<GetSelectListQuery, CreateGarmentInputDto>
{
    private readonly IApplicationDbContext dbContext;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IMediator mediator;
    private readonly IHttpContextAccessor httpContextAccessor;

    public GetAddSelectListQueryHandler(IApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IMediator mediator)
    {
        this.dbContext = dbContext;
        this.mediator = mediator;
        this.userManager = userManager;
        this.httpContextAccessor = httpContextAccessor;

    }

    public async Task<CreateGarmentInputDto> Handle(GetSelectListQuery request, CancellationToken cancellationToken)
    {
        var user = await this.userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
        // todo:
        //var categoryNamesQuery = new();
        //var seasonNamesQuery = new();
        //var colourNamesQuery = new();

        //var createGarmentInputDto = new CreateGarmentInputDto()
        //{
        //    CategoryNames = await this.mediator.Send(categoryNamesQuery);
        //    SeasonNames = await this.mediator.Send(seasonNamesQuery);
        //    ColourNames = await this.mediator.Send(colourNamesQuery);
        //};


        var query = new GetCategoriesByUserIdQuery();
        var getAllCategoriesByUserId = await this.mediator.Send(query, cancellationToken);

        async Task<SelectList> CategoryList()
        {
            var categories = await dbContext.UsersCategories
                .Where(x => x.ApplicationUserId == user.Id)
                .Select(x => x.Category).OrderBy(x => x.Name).ToListAsync(cancellationToken);

            //var categoriesSelectList = new SelectList(categories, "CategoryId", "Name");

            return new SelectList(categories, "CategoryId", "Name");
        }

        async Task<SelectList> SeasonList()
        {
            var seasons = await dbContext.Seasons.OrderBy(x => x.Name).ToListAsync(cancellationToken);
            //.Where(x => x.ApplicationUserId == userId)
            //.Select(x => x.Seasons).OrderBy(x => x.Name).ToListAsync();

            //var seasonsSelectList = new SelectList(seasons, "SeasonId", "Name");

            return new SelectList(seasons, "SeasonId", "Name");
        }

        async Task<SelectList> ColourList()
        {
            var colours = await dbContext.Colours.OrderBy(x => x.Name).ToListAsync(cancellationToken);
            //.Where(x => x.ApplicationUserId == userId)
            //.Select(x => x.Colours).OrderBy(x => x.Name).ToListAsync();

            return new SelectList(colours, "ColourId", "Name");
        }
        var createInputDto = new CreateGarmentInputDto()
        {
            CategoryNames = await CategoryList(),
            SeasonNames = await SeasonList(),
            ColourNames = await ColourList()
        };

        return createInputDto;
    }
}
