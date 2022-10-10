namespace MyClothesCA.Domain.Entities
{
    using System;
    using Microsoft.AspNetCore.Identity;
    public class ApplicationUser : IdentityUser//, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {

            this.Id = Guid.NewGuid().ToString();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Clothes = new HashSet<UserClothes>();
            this.UserClothesCollection = new HashSet<Garment>();
            this.UserCategoriesList = new HashSet<UserCategory>();

        }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
        public ICollection<UserClothes> Clothes { get; set; }
        public ICollection<Garment> UserClothesCollection { get; set; }
        public ICollection<UserCategory> UserCategoriesList { get; set; }

    }
}

