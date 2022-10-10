using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MyClothesCA.Application.Common.Interfaces;
using MyClothesCA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyClothesCA.Application.DTOs.ClothesDTOs;
using MyClothesCA.Application.Clothes.Commands.EditPicture;

namespace MyClothesCA.Application.Clothes.Commands.DeletePicture;
public class DeletePictureCommand:IRequest
{
    public DeletePictureCommand(string garmentId)
    {
        this.Id = garmentId;
    }
    public string Id { get; set; }
}
public class DeletePictureCommandHandler : IRequestHandler<DeletePictureCommand,Unit>
{
    private readonly IApplicationDbContext dbContext;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor httpContextAccessor;
    public DeletePictureCommandHandler(IApplicationDbContext dbContext, IMapper mapper, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.userManager = userManager;
        this.httpContextAccessor = httpContextAccessor;

    }

    public async Task<Unit> Handle(DeletePictureCommand request, CancellationToken cancellationToken)
    {
        var user = await this.userManager.GetUserAsync(httpContextAccessor.HttpContext?.User);

        var userId = await this.userManager.GetUserIdAsync(user);


        Garment garment = await this.dbContext.Clothes.FindAsync(request.Id);

        var image = await this.dbContext.Images.FindAsync(garment.ImageId);

        this.dbContext.Clothes.Remove(garment);

        this.dbContext.Images.Remove(image);

        var path = $"wwwroot/{image.RemoteImageUrl}";

        //var path = Path.Combine(Directory.GetCurrentDirectory(), "~", image.RemoteImageUrl);

        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
        await this.dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
