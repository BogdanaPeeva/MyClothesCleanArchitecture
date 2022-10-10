using System;
namespace MyClothesCA.Domain.Entities
{
    public class UserCategory
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

