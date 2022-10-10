using System;
using System.ComponentModel.DataAnnotations;

namespace MyClothesCA.Domain.Entities
{
    public class Image
    {

        [Key]
        public string ImageId { get; set; } = Guid.NewGuid().ToString();
        public string Extension { get; set; }

        //// The contents of the image is in the file system
        [Required]
        public string RemoteImageUrl { get; set; }

        public string AddedByApplicationUserId { get; set; }

        public ApplicationUser AddedByApplicationUser { get; set; }

    }
}

