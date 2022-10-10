using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyClothesCA.Application.Categories.Commands.CreateCategory;
using MyClothesCA.Application.Categories.Commands.DeleteCategory;
using MyClothesCA.Application.Categories.Commands.EditCategory;
using MyClothesCA.Application.Categories.Queries.GetCategories;
using MyClothesCA.Application.Categories.Queries.GetEditCategories;
using MyClothesCA.Application.DTOs.CategoriesDTOs;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class CategoriesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly ISender sender;

        public CategoriesController(
       IMapper mapper,
      IMediator mediator, ISender sender
   )
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.sender = sender;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var query = new GetCategoriesByUserIdQuery();

                var allCategoriesVMList = await this.mediator.Send(query);

                return this.View(allCategoriesVMList);

            }
            catch (Exception exception)
            {
                this.TempData["Message"] = exception.Message;

                return this.RedirectToAction(nameof(this.All));
            }

        }
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return  this.View();

        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCategoryInputDto inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            try
            {
                 await this.mediator.Send(new CreateCategoryCommand(inputModel));

                this.TempData["Message"] = "Category was successfully aded.";

            }
            catch (Exception ex)
            {
                this.TempData["Message"] = ex.Message;

                return this.RedirectToAction(nameof(this.Create));
            }

            return this.RedirectToAction(nameof(this.Index));
        }
        [Authorize]
        public async Task<IActionResult> All()
        {
            try
            {
                var query = new GetCategoriesByUserIdQuery();

                var allCategoriesVMList = await this.mediator.Send(query);

                return this.View(allCategoriesVMList);
            }

            catch (Exception ex)
            {
                this.TempData["Message"] = ex.Message;


                return this.RedirectToAction(nameof(this.Create));
            }

        }
        [Authorize]
        public async Task<IActionResult> ByName(string name)
        {
            try
            {
                var query = new GetByNameQuery(name);
                var allClothesVm = await this.mediator.Send(query);

                return this.View(allClothesVm);
            }
            catch (Exception exception)
            {
                this.TempData["Message"] = exception.Message;

                return RedirectToAction(nameof(this.Index));
            }

        }
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var query = new DeleteCategoryCommand(id);

                await this.mediator.Send(query);

                return RedirectToAction(nameof(this.Index));
            }
            catch (Exception exception)
            {
                this.TempData["Message"] = exception.Message;

                return RedirectToAction(nameof(this.Index));
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var query = new GetEditCategoryQuery(id);

                var editCategoryDto = await this.mediator.Send(query);

                return this.View(editCategoryDto);
            }

            catch (Exception exception)
            {

                this.TempData["Message"] = exception.Message;

                return RedirectToAction(nameof(this.Index));
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(string id, EditCategoryDto editCategoryDto)
        {

            if (!this.ModelState.IsValid)
            {
                return this.View(editCategoryDto);
            }
            try
            {
                var editedDto = await this.mediator.Send(new EditCategoryCommand(id, editCategoryDto));

                this.TempData["Message"] = "Picture was edit successfully.";

                return RedirectToAction(nameof(this.All), new { id = editedDto.CategoryId });
            }
            catch (Exception exception)
            {
                this.TempData["Message"] = exception.Message;

                return RedirectToAction(nameof(this.Index));
            }
        }
    }
}

