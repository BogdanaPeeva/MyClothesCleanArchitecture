using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MyClothesCA.Application.Common.Interfaces;
using MyClothesCA.Domain.Entities;
using MyClothesCA.Application.DTOs.ClothesDTOs;

namespace MyClothesCA.Application.Clothes.Commands.EditPicture;
public class EditPictureCommand: IRequest<EditGarmentDto>
{
    public EditPictureCommand(string id,EditGarmentDto editGarmentDto)
    {
        this.Id = id;
        this.EditGarmentDto = editGarmentDto;
    }
    public string Id { get; set; }
    public EditGarmentDto EditGarmentDto { get; set; }
}
public class EditPictureCommandHandler : IRequestHandler<EditPictureCommand, EditGarmentDto>
{
    private readonly IApplicationDbContext dbContext;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor httpContextAccessor;
    public EditPictureCommandHandler(IApplicationDbContext dbContext, IMapper mapper, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.userManager = userManager;
        this.httpContextAccessor = httpContextAccessor;

    }
    public async Task<EditGarmentDto> Handle(EditPictureCommand request, CancellationToken cancellationToken)
    {
        var user = await this.userManager.GetUserAsync(httpContextAccessor.HttpContext?.User);


        Garment garment = await this.dbContext.Clothes.FindAsync(request.Id);

        if (garment == null)
        {
            throw new Exception("Garment doesn't exist!");
        }

        Garment updatedModel = this.mapper.Map(request.EditGarmentDto, garment);

         this.dbContext.Clothes.Update(updatedModel);
        
        await this.dbContext.SaveChangesAsync(cancellationToken);

        EditGarmentDto editGarmentDto = this.mapper.Map<EditGarmentDto>(updatedModel);

        return editGarmentDto;
    }
}