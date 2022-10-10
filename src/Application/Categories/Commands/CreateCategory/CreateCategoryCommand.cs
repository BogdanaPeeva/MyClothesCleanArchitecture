using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyClothesCA.Application.Common.Interfaces;
using MyClothesCA.Application.DTOs.CategoriesDTOs;
using MyClothesCA.Application.DTOs.ClothesDTOs;
using MyClothesCA.Domain.Entities;

namespace MyClothesCA.Application.Categories.Commands.CreateCategory;
public class CreateCategoryCommand:IRequest
{
    public CreateCategoryCommand(CreateCategoryInputDto createCategoryInputDto)
    {
        this.CreateCategoryInputDto = createCategoryInputDto;
    }
    public CreateCategoryInputDto CreateCategoryInputDto { get; set; }
}
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Unit>
{
    private readonly IApplicationDbContext dbContext;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor httpContextAccessor;
    public CreateCategoryCommandHandler(IApplicationDbContext dbContext, IMapper mapper, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.userManager = userManager;
        this.httpContextAccessor = httpContextAccessor;

    }

    public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var user = await this.userManager.GetUserAsync(httpContextAccessor.HttpContext?.User);
        var userId = await this.userManager.GetUserIdAsync(user);

        var category = new Category
        {
            Name = request.CreateCategoryInputDto.CategoryName.Trim(' '),
        };

        if (await this.dbContext.UsersCategories.FirstOrDefaultAsync(x => x.ApplicationUserId == userId && x.CategoryId == category.CategoryId) != null)
        {
            throw new Exception("This category already exist!");
        }

        var userCategory = new UserCategory()
        {
            ApplicationUserId = userId,
            CategoryId = category.CategoryId,
        };

        await this.dbContext.UsersCategories.AddAsync(userCategory);

        if (await dbContext.Categories.FirstOrDefaultAsync(x => x.Name == category.Name) != null)
        {
            throw new Exception("This category already exist!");
        }
        else
        {
            await dbContext.Categories.AddAsync(category);

        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
