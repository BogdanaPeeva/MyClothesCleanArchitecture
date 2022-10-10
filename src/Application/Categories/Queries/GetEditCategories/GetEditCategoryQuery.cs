using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MyClothesCA.Application.Categories.Queries.GetCategories;
using MyClothesCA.Application.Clothes.Queries.GetEditGarments;
using MyClothesCA.Application.Common.Interfaces;
using MyClothesCA.Application.DTOs.CategoriesDTOs;
using MyClothesCA.Application.DTOs.ClothesDTOs;
using MyClothesCA.Domain.Entities;

namespace MyClothesCA.Application.Categories.Queries.GetEditCategories;
public class GetEditCategoryQuery:IRequest<EditCategoryDto>
{
    public GetEditCategoryQuery(string id)
    {
        this.Id = id;

    }
    public string Id { get; }

}
public class GetEditCategoryQueryHandler : IRequestHandler<GetEditCategoryQuery, EditCategoryDto>
{
    private readonly IApplicationDbContext dbContext;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IMapper mapper;
    private readonly IMediator mediator;
    private readonly IHttpContextAccessor httpContextAccessor;

    public GetEditCategoryQueryHandler(IApplicationDbContext dbContext, IMapper mapper, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IMediator mediator)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.mediator = mediator;
        this.userManager = userManager;
        this.httpContextAccessor = httpContextAccessor;

    }

    public async Task<EditCategoryDto> Handle(GetEditCategoryQuery request, CancellationToken cancellationToken)
    {
        var user = await this.userManager.GetUserAsync(httpContextAccessor.HttpContext?.User);

        var userId = await this.userManager.GetUserIdAsync(user);

        var id = request.Id;

        if (id == "1" || id == "2" || id == "3"
                || id == "4" || id == "5" || id == "6"
                || id == "7")
        {
            throw new Exception("Can not edit this category!");

        }

        Category category = await this.dbContext.Categories.FindAsync(request.Id);

        if (category == null)
        {
            throw new Exception("The category doesn't exist!");
        }

        EditCategoryDto editCategoryDto = this.mapper.Map<EditCategoryDto>(category);

        return editCategoryDto;
    }
}