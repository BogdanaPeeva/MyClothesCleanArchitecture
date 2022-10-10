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
using MyClothesCA.Application.DTOs.CategoriesDTOs;
using MyClothesCA.Domain.Entities;

namespace MyClothesCA.Application.Categories.Queries.GetCategories;
public class GetCategoriesByUserIdQuery : IRequest<AllCategoriesVm>
{

}
public class GetCategoriesByUserIdQueryHandler : IRequestHandler<GetCategoriesByUserIdQuery, AllCategoriesVm>
{
    private readonly IApplicationDbContext dbContext;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor httpContextAccessor;

    public GetCategoriesByUserIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.userManager = userManager;
        this.httpContextAccessor = httpContextAccessor;

    }

    public async Task<AllCategoriesVm> Handle(GetCategoriesByUserIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext?.User);

        var userId = await userManager.GetUserIdAsync(user);

        var categories = await dbContext.Categories.ToListAsync(cancellationToken);

        var userCategories = await dbContext.UsersCategories.Where(x => x.ApplicationUserId == user.Id).ToListAsync(cancellationToken);

        if (userCategories.Count == 0)
        {
            foreach (var categoryIn in categories)
            {
                var categoryId = categoryIn.CategoryId;

                if (categoryId == "1" || categoryId == "2" || categoryId == "3"
                    || categoryId == "4" || categoryId == "5" || categoryId == "6"
                    || categoryId == "7")
                {

                    if (await dbContext.UsersCategories.FirstOrDefaultAsync(x => x.ApplicationUserId == user.Id && x.CategoryId == categoryId) == null)
                    {
                        await dbContext.UsersCategories.AddAsync(new UserCategory
                        {
                            CategoryId = categoryId,
                            ApplicationUserId = userId,
                        });
                    }
                }
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        var createCategoryViewModelList = await dbContext.UsersCategories.Where(x => x.ApplicationUserId == userId)
            .Include(x => x.Category)
            .Select(i => new CategoryDto()
            {
                CategoryId = i.CategoryId,

                CategoryName = i.Category.Name,

                ImageUrl = i.Category.ImageUrl,

                CategoryClothesCollectionCount = dbContext.Clothes
            .Where(x => x.ApplicationUserId == userId && x.CategoryId == i.CategoryId).Count(),
            })
           .OrderBy(x => x.CategoryName)
           .ToListAsync(cancellationToken);

        var allCategoriesVM = new AllCategoriesVm
        {
            Categories = createCategoryViewModelList
        };

        return allCategoriesVM;
    }
}


