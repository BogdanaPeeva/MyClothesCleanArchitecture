using System;
using System.ComponentModel.DataAnnotations;

namespace MyClothesCA.Domain.Entities
{
    public class Garment
    {
        [Key]
        public string GarmentId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string ImageUrl { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public string? CategoryId { get; set; }
        public Category? Category { get; set; }
        public string? SeasonId { get; set; }
        public Season? Season { get; set; }

        public string? ColourId { get; set; }
        public Colour? Colour { get; set; }

        [Required]
        public string ImageId { get; set; }


    }
}

