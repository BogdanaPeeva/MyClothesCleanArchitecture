using System;

namespace MyClothesCA.Application.DTOs.ClothesDTOs
{
    public class GarmentDto
    {
        public string GarmentId { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; } = "N/A";
        public string SeasonName { get; set; } = "N/A";
        public string ColourName { get; set; } = "N/A";

        public string Description { get => $"Category: {CategoryName ?? "not aded"} <br> Season: {SeasonName ?? "not aded "} <br> Colour: {ColourName ?? "not aded"}"; }
    }
}

