using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyClothesCA.Application.Common.Interfaces;
using MyClothesCA.Domain.Entities;

namespace MyClothesCA.Application.Categories.Commands.DeleteCategory;
public class DeleteCategoryCommand : IRequest
{
    public DeleteCategoryCommand(string id)
    {
        this.Id = id;

    }
    public string Id { get; }

}
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
    private readonly IApplicationDbContext dbContext;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor httpContextAccessor;
    public DeleteCategoryCommandHandler
        (IApplicationDbContext dbContext, IMapper mapper, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.userManager = userManager;
        this.httpContextAccessor = httpContextAccessor;

    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var user = await this.userManager.GetUserAsync(httpContextAccessor.HttpContext?.User);

        var userId = await this.userManager.GetUserIdAsync(user);

        var id = request.Id;

        Category category = await this.dbContext.Categories
            .FindAsync(id);

        UserCategory userCategory = await this.dbContext.UsersCategories.Where(x => x.ApplicationUserId == userId && x.CategoryId == request.Id).FirstOrDefaultAsync(cancellationToken);


        if (id != "1" && id != "2" && id != "3"
                    && id != "4" && id != "5" && id != "6"
                    && id != "7")
        {
            dbContext.Categories.Remove(category);

            dbContext.UsersCategories.Remove(userCategory);

            await this.dbContext.SaveChangesAsync(cancellationToken);

            throw new Exception("Category was succesfully deleted!");
        }
        else
        {
            throw new Exception("Can not delete this category!");
        }

    }
}