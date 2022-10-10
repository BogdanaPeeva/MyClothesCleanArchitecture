using System;
using System.ComponentModel.DataAnnotations;

namespace MyClothesCA.Domain.Entities
{
    public class Category
    {

        [Key]
        public string CategoryId { get; set; } = Guid.NewGuid().ToString();

        [StringLength(15)]
        public string? Name { get; set; }

        public string? ImageUrl { get; set; }
    }
}


