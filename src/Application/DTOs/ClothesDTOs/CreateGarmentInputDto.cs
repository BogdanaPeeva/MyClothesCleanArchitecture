namespace MyClothesCA.Application.DTOs.ClothesDTOs
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Linq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public class CreateGarmentInputDto
    {
        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "Please select a file!(.jpg, .jpeg, .png, .avi)")]
        public IFormFile Images { get; set; }

        [Display(Name = "Category")]
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }

        public SelectList CategoryNames { get; set; }

        [Display(Name = "Season")]
        public string SeasonId { get; set; }
        public string SeasonName { get; set; }
        public SelectList SeasonNames { get; set; }

        [Display(Name = "Colour")]
        public string ColourId { get; set; }
        public string ColourName { get; set; }
        public SelectList ColourNames { get; set; }
    }
}

