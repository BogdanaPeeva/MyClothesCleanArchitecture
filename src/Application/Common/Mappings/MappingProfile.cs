using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyClothesCA.Application.DTOs.CategoriesDTOs;
using MyClothesCA.Application.DTOs.ClothesDTOs;
using MyClothesCA.Domain.Entities;

namespace MyClothesCA.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Clothes Map

        CreateMap<Garment, GarmentDto>();
        CreateMap<CreateGarmentInputDto, Garment>();

        CreateMap<Garment, CreateGarmentInputDto>();


        CreateMap<GarmentDto, Garment>();
        CreateMap<GarmentDto, CreateGarmentInputDto>();

        //todo: remove
        //CreateMap<CreateGarmentInputDto, GarmentDto>();

        CreateMap<EditGarmentDto, Garment>();

        CreateMap<Garment, EditGarmentDto>();

        CreateMap<CreateGarmentInputDto, SelectList>();
        CreateMap<SelectList, CreateGarmentInputDto>();

        CreateMap<AllClothesVm, ICollection<GarmentDto>>().ReverseMap();

        // todo: try reverceMap
        // Category Map

        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryDto, Category>();

        CreateMap<Category, CreateGarmentInputDto>();
        CreateMap<CreateGarmentInputDto, Category>();

        CreateMap<AllCategoriesVm, ICollection<CategoryDto>>().ReverseMap();

        CreateMap<Category, CreateCategoryInputDto>();
        CreateMap<CreateCategoryInputDto, Category>();

        CreateMap<EditCategoryDto, Category>();
        CreateMap<Category, EditCategoryDto>();

        // Season Map

        CreateMap<Season, CreateGarmentInputDto>();
        CreateMap<CreateGarmentInputDto, Season>();

        //Colour Map

        CreateMap<Colour, CreateGarmentInputDto>();

        CreateMap<CreateGarmentInputDto, Colour>();


        //CreateMap<Season, SeasonViewModel>();
        //CreateMap<Colour, ColourViewModel>();

    }
}
