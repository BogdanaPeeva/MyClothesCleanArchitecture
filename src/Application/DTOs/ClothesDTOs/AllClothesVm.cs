namespace MyClothesCA.Application.DTOs.ClothesDTOs
{
    using System;

    public class AllClothesVm
    {

        public IList<GarmentDto> ClothesModelList { get; set; } = new List<GarmentDto>();

    }
}

