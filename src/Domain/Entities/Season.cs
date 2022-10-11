using System.ComponentModel.DataAnnotations;

namespace MyClothesCA.Domain.Entities
{
    public class Season
    {
        [Key]
        public string SeasonId { get; set; } = Guid.NewGuid().ToString();

        [StringLength(15)]
        public string? Name { get; set; }


    }
}

