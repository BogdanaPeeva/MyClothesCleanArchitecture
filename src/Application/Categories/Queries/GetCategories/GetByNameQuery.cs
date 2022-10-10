using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyClothesCA.Application.Common.Interfaces;
using MyClothesCA.Application.DTOs.ClothesDTOs;
using MyClothesCA.Domain.Entities;

namespace MyClothesCA.Application.Categories.Queries.GetCategories;
public class GetByNameQuery:IRequest<AllClothesVm>
{
    public GetByNameQuery(string name)
    {
        this.Name = name;
    }
    public string Name { get; }

}
public class GetByNameQueryHandler : IRequestHandler<GetByNameQuery, AllClothesVm>
{
    private readonly IApplicationDbContext dbContext;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IMapper mapper;
    private readonly IMediator mediator;
    private readonly IHttpContextAccessor httpContextAccessor;

    public GetByNameQueryHandler(IApplicationDbContext dbContext, IMapper mapper, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IMediator mediator)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.mediator = mediator;
        this.userManager = userManager;
        this.httpContextAccessor = httpContextAccessor;

    }

    public async Task<AllClothesVm> Handle(GetByNameQuery request, CancellationToken cancellationToken)
    {
        var user = await this.userManager.GetUserAsync(httpContextAccessor.HttpContext?.User);

        var userId = await this.userManager.GetUserIdAsync(user);

        Category category = await this.dbContext.Categories.FirstOrDefaultAsync(x => x.Name ==request.Name);

        string categoryId = category.CategoryId;

        if (category == null)
        {
            throw new Exception("No pictures in this category!");
        }

        var garmentList = new List<Garment>();

        garmentList = await this.dbContext.Clothes.Where(x => x.CategoryId == categoryId && x.ApplicationUserId == userId).ToListAsync(cancellationToken);

        List<GarmentDto> garmentDtosList = this.mapper.Map<List<GarmentDto>>(garmentList);

        var allPicturesVM = new AllClothesVm()
        {
           ClothesModelList = garmentDtosList
        };

        return allPicturesVM;
    }
}