using System.ComponentModel.DataAnnotations;

namespace MyClothesCA.Domain.Entities
{
    public class Colour
    {
        [Key]
        public string ColourId { get; set; } = Guid.NewGuid().ToString();
        [StringLength(15)]
        public string? Name { get; set; }
    }
}

