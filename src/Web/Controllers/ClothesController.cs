
using static System.Net.Mime.MediaTypeNames;

namespace Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using MediatR;
using MyClothesCA.Application.Clothes.Queries.GetClothes;
using MyClothesCA.Application.Clothes.Queries.GetEditGarments;
using MyClothesCA.Application.Clothes.Commands.EditPicture;
using MyClothesCA.Application.DTOs.ClothesDTOs;
using MyClothesCA.Application.Clothes.Commands.DeletePicture;
using MyClothesCA.Application.Clothes.Commands.CreatePicture;

[AutoValidateAntiforgeryToken]
public class ClothesController : Controller
{
    
    private readonly IWebHostEnvironment environment;
    private readonly IMapper mapper;
    private readonly IMediator mediator;
    private readonly ISender sender;

    private readonly string wwwrootDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//images");

    public ClothesController(
        IMapper mapper,
        IWebHostEnvironment environment,
       IMediator mediator,ISender sender
    )
    {
        
        this.environment = environment;
        this.mapper = mapper;
        this.mediator = mediator;
        this.sender = sender;
    }

    [Authorize]
    public IActionResult Index()
    {
        return this.View();
    }
  
    [Authorize]
    public async Task<IActionResult> Add()
    {
        var query =new GetSelectListQuery();

        var createGarmentInputDto = await this.mediator.Send(query);
      
        return this.View(createGarmentInputDto);
    }
   
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add(CreateGarmentInputDto createGarmentInputDto)
    {

        try
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(createGarmentInputDto);
            }

            var garmentId = await this.mediator.Send(new AddPictureCommand (createGarmentInputDto));

            this.TempData["Message"] = "Picture added successfully.";

            return RedirectToAction(nameof(this.All), new { id = garmentId});
        }
        catch (Exception exception)
        {
            var query = new GetSelectListQuery();

            var createGarmInputDto = await this.mediator.Send(query);

            this.TempData["Message"] = exception.Message;

            //return this.View(await this.mediator.Send(new GetSelectListQuery()));

            return this.View(createGarmentInputDto);
        }
    }
   
    [Authorize]
    public async Task<IActionResult> Details(string id)
    {
        try
        {

            var query = new GetGarmentDetailsQuery(id);

            var garmentDto = await this.mediator.Send(query);

            return this.View(garmentDto);

        }
        catch (Exception exception)
        {
            this.TempData["Message"] = exception.Message;

            return RedirectToAction(nameof(this.All));
        }
    }

    [Authorize]
    public async Task<IActionResult> All()
    {
        try
        {
            var query = new GetAllClothesQuery();

            var allPicturesViewModel = await this.mediator.Send(query);

            return this.View(allPicturesViewModel);


        }
        catch (Exception exception)
        {

            this.TempData["Message"] = exception.Message;

            return this.RedirectToAction(nameof(this.Index));

        }
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Edit(string id)
    {
        try
        {
            var query = new GetEditGarmentQuery(id);

            var editGarmentDto= await this.mediator.Send(query);

            return this.View(editGarmentDto);
        }
        catch (Exception exception)
        {
            this.TempData["Message"] = exception.Message;

            return RedirectToAction(nameof(this.All));

        };
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Edit(string id, EditGarmentDto editGarmentdto)
    {

        if (!this.ModelState.IsValid)
        {
            return this.View(editGarmentdto);
        }
        try
        {
            var editPictureDto = await this.mediator.Send(new EditPictureCommand(id, editGarmentdto));
            this.TempData["Message"] = "Picture was edit successfully.";

            return RedirectToAction(nameof(this.Details), new { id = editPictureDto.GarmentId });
        }
        catch (Exception exception)
        {

            this.TempData["Message"] = exception.Message;

            return RedirectToAction(nameof(this.All));
        }
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {

        var query = new GetGarmentDetailsQuery(id);

        var garmentDto = await this.mediator.Send(query);

        return this.View(garmentDto);

    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> DeletePost(string garmentId)
    {
        try
        {
            var editPictureDto = await this.mediator.Send(new DeletePictureCommand(garmentId));

            return RedirectToAction(nameof(this.All));
        }
        catch (Exception exception)
        {

            this.TempData["Message"] = exception.Message;

            return RedirectToAction(nameof(this.All));
        }
    }

}

