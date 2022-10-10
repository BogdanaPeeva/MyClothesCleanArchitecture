using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using MyClothesCA.Application.Common.Interfaces;
using MyClothesCA.Domain.Entities;
using MyClothesCA.Application.DTOs.ClothesDTOs;

namespace MyClothesCA.Application.Clothes.Queries.GetClothes;

public class GetGarmentDetailsQuery : IRequest<GarmentDto>
{
    public string Id { get; }

    public GetGarmentDetailsQuery(string id)
    {
        this.Id = id;
    }
}
public class GetGarmentDetailsQueryHandler : IRequestHandler <GetGarmentDetailsQuery,GarmentDto>
{
    private readonly IApplicationDbContext dbContext;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor httpContextAccessor;

    public GetGarmentDetailsQueryHandler(IApplicationDbContext dbContext, IMapper mapper, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.userManager = userManager;
        this.httpContextAccessor = httpContextAccessor;

    }

    public async Task<GarmentDto> Handle(GetGarmentDetailsQuery request, CancellationToken cancellationToken)
    {
        var user = await this.userManager.GetUserAsync(httpContextAccessor.HttpContext.User);

        var userId = await this.userManager.GetUserIdAsync(user);


        Garment garment = await this.dbContext.Clothes.Where(x => x.GarmentId == request.Id)
            .Include(x => x.Category)
            .Include(x => x.Season)
            .Include(x => x.Colour).FirstOrDefaultAsync(cancellationToken);

        if (garment == null)
        {
            throw new Exception("This garment doesn't exist!");
        }


        GarmentDto garmentDto = this.mapper.Map<GarmentDto>(garment);

        return garmentDto;
    }

}


