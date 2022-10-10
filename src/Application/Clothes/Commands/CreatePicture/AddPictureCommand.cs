using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyClothesCA.Application.Common.Interfaces;
using MyClothesCA.Application.DTOs.ClothesDTOs;
using MyClothesCA.Domain.Entities;

namespace MyClothesCA.Application.Clothes.Commands.CreatePicture;

public class AddPictureCommand : IRequest<string>
{
    public AddPictureCommand(CreateGarmentInputDto createGarmentInputDto)
    {
        CreateGarmentInputDto = createGarmentInputDto;
    }
    public CreateGarmentInputDto CreateGarmentInputDto { get; set; }

}

public class AddPictureCommandHandler : IRequestHandler<AddPictureCommand, string>
{
    private readonly IApplicationDbContext dbContext;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IWebHostEnvironment environment;

    private readonly string[] allowedExtensions = new[] { "jpg", "jpeg", "png", "gif", "jfif", "avif" };

    public AddPictureCommandHandler(IApplicationDbContext dbContext, IMapper mapper, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.userManager = userManager;
        this.httpContextAccessor = httpContextAccessor;
        this.environment = environment;

    }

    public async Task<string> Handle(AddPictureCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext?.User);

        var userId = await userManager.GetUserIdAsync(user);

        var path = $"{environment.WebRootPath}/images";

        var garment = new Garment
        {
            ApplicationUserId = userId,
            ApplicationUser = user
        };

        if (request.CreateGarmentInputDto.CategoryId != default)
        {
            garment.CategoryId = request.CreateGarmentInputDto.CategoryId;

            garment.Category = await dbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == request.CreateGarmentInputDto.CategoryId);

            garment.Category.Name = dbContext.Categories.FirstOrDefault(x => x.CategoryId == request.CreateGarmentInputDto.CategoryId).Name;
        }
        if (request.CreateGarmentInputDto.SeasonId != default)
        {
            garment.SeasonId = request.CreateGarmentInputDto.SeasonId;

            garment.Season = await dbContext.Seasons.FirstOrDefaultAsync(x => x.SeasonId == request.CreateGarmentInputDto.SeasonId);

            garment.Season.Name = dbContext.Seasons.FirstOrDefault(x => x.SeasonId == request.CreateGarmentInputDto.SeasonId).Name;
        }

        if (request.CreateGarmentInputDto.ColourId != default)
        {

            garment.ColourId = request.CreateGarmentInputDto.ColourId;

            garment.Colour = await dbContext.Colours.FirstOrDefaultAsync(x => x.ColourId == request.CreateGarmentInputDto.ColourId);

            garment.Colour.Name = dbContext.Colours.FirstOrDefault(x => x.ColourId == request.CreateGarmentInputDto.ColourId).Name;

        }

        // /wwwroot/images/clothes/jhdsi-343g3h453-=g34g.jpg

        if (request.CreateGarmentInputDto.Images != default)
        {

            Directory.CreateDirectory($"{path}/clothes/");

            var extension = Path.GetExtension(request.CreateGarmentInputDto.Images.FileName).TrimStart('.');

            if (!allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            var dbImage = new Image
            {

                AddedByApplicationUserId = userId,
                Extension = extension,

            };

            if (dbImage.RemoteImageUrl == null)
            {
                dbImage.RemoteImageUrl = "/images/clothes/" + dbImage.ImageId + "." + dbImage.Extension;
            }

            garment.ImageId = dbImage.ImageId;

            var physicalPath = $"{path}/clothes/{dbImage.ImageId}.{extension}";

            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);

            await request.CreateGarmentInputDto.Images.CopyToAsync(fileStream);

            garment.ImageUrl = dbImage.RemoteImageUrl;

            await dbContext.Images.AddAsync(dbImage);

        }
        var garmentDto = new GarmentDto();

        garmentDto = mapper.Map<GarmentDto>(garment);

        if (await dbContext.Clothes.FirstOrDefaultAsync(x => x.GarmentId == garment.GarmentId) != null)
        {
            throw new Exception("Picture already exist!");
        }

        var userGarment = new UserClothes()
        {
            ApplicationUserId = userId,
            GarmentId = garment.GarmentId
        };

        await dbContext.Clothes.AddAsync(garment);

        await dbContext.UserClothes.AddAsync(userGarment);

        await dbContext.SaveChangesAsync(cancellationToken);

        return garmentDto.GarmentId;

    }
}

