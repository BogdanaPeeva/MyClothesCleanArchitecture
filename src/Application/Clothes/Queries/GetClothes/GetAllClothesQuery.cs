using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using MyClothesCA.Application.Common.Interfaces;
using MyClothesCA.Domain.Entities;
using MyClothesCA.Application.DTOs.ClothesDTOs;

namespace MyClothesCA.Application.Clothes.Queries.GetClothes;

public class GetAllClothesQuery : IRequest<AllClothesVm>
{
}
public class GetAllClothesQueryHandler : IRequestHandler<GetAllClothesQuery, AllClothesVm>
{
    private readonly IApplicationDbContext dbContext;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor httpContextAccessor;

    public GetAllClothesQueryHandler(IApplicationDbContext dbContext, IMapper mapper, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.userManager = userManager;
        this.httpContextAccessor = httpContextAccessor;

    }

    public async Task<AllClothesVm> Handle(GetAllClothesQuery request, CancellationToken cancellationToken)
    {
        var user = await this.userManager.GetUserAsync(httpContextAccessor.HttpContext?.User);

        var userId = await this.userManager.GetUserIdAsync(user);


        if (await dbContext.UserClothes.FirstOrDefaultAsync(x => x.ApplicationUserId == userId) == null)
        {
            throw new Exception("No pictures! Please add pictures!");
        }

        List<Garment> garmentList = await this.dbContext.Clothes.Where(x => x.ApplicationUserId == userId)
            .Include(x => x.Category)
            .Include(x => x.Season)
            .Include(x => x.Colour)
            .OrderBy(g => g.GarmentId)
            .ToListAsync(cancellationToken);

        List<GarmentDto> garmentModel = this.mapper.Map<List<GarmentDto>>(garmentList);


        var allClothesVM = new AllClothesVm()
        {
            ClothesModelList = garmentModel
        };

        return allClothesVM;
    }
}

