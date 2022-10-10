namespace MyClothesCA.Domain.Entities
{
    using System;

    public class UserClothes
    {
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string GarmentId { get; set; }

        public Garment Garment { get; set; }
    }
}

