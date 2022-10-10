using AutoMapper;
using MediatR;
using MyClothesCA.Application.Common.Interfaces;
using MyClothesCA.Application.DTOs.CategoriesDTOs;
using MyClothesCA.Domain.Entities;

namespace MyClothesCA.Application.Categories.Commands.EditCategory;
public class EditCategoryCommand:IRequest<EditCategoryDto>
{
    public EditCategoryCommand(string id,EditCategoryDto editCategoryDto)
    {
        this.Id = id;
        this.EditCategoryDto = editCategoryDto;
    }
    public string Id { get; }
    public EditCategoryDto EditCategoryDto { get; }
}
public class EditCategoryCommandHandler : IRequestHandler<EditCategoryCommand, EditCategoryDto>
{
    private readonly IApplicationDbContext dbContext;
    private readonly IMapper mapper;
    public EditCategoryCommandHandler(IApplicationDbContext dbContext,
        IMapper mapper)
        
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        
    }

    public async Task<EditCategoryDto> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        Category category = await this.dbContext.Categories.FindAsync(request.Id);

        if (category == null)
        {
            throw new Exception("The category doesn't exist!");
        }

        EditCategoryDto editCategoryDtoTrimed = request.EditCategoryDto;

        editCategoryDtoTrimed.Name =request.EditCategoryDto.Name.Trim(' ');

        Category updatedModel = this.mapper.Map(editCategoryDtoTrimed, category);

       this.dbContext.Categories.Update(updatedModel);

        await this.dbContext.SaveChangesAsync(cancellationToken);

        EditCategoryDto editCategoryDto = this.mapper.Map<EditCategoryDto>(updatedModel);

        return editCategoryDto;
    }
}
