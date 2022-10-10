using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using MyClothesCA.Application.Common.Interfaces;
using MyClothesCA.Domain.Entities;
using MyClothesCA.Application.DTOs.ClothesDTOs;

namespace MyClothesCA.Application.Clothes.Queries.GetClothes;

public class GetDeleteQuery : IRequest<GarmentDto>
{
    public string Id { get; }

    public GetDeleteQuery(string id)
    {
        Id = id;

    }

}
public class GetDeleteQueryHandler : IRequestHandler<GetDeleteQuery, GarmentDto>
{
    private readonly IApplicationDbContext dbContext;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor httpContextAccessor;

    public GetDeleteQueryHandler(IApplicationDbContext dbContext, IMapper mapper, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.userManager = userManager;
        this.httpContextAccessor = httpContextAccessor;

    }

    public async Task<GarmentDto> Handle(GetDeleteQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);

        var userId = await userManager.GetUserIdAsync(user);

        Garment garment = await dbContext.Clothes.Where(x => x.GarmentId == request.Id).Include(x => x.Category).Include(x => x.Season).Include(x => x.Season).FirstOrDefaultAsync(cancellationToken);

        GarmentDto garmentDto = mapper.Map<GarmentDto>(garment);

        return garmentDto;
    }
}