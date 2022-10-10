using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyClothesCA.Application.DTOs.CategoriesDTOs;
public class EditCategoryDto
{
    public string CategoryId { get; set; }

    [Display(Name = "Category Name")]
    [Required(ErrorMessage = "Shoud be beetween 3 and 20 symbols! ")]
    [MinLength(3)]
    [MaxLength(20),]
    public string Name { get; set; }
    public string? Url => $"/c/{this.Name.Replace(" ", "-").Trim('-')}";
}
