using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyClothesCA.Application.DTOs.CategoriesDTOs;
public class CreateCategoryInputDto
{
    [MinLength(3)]
    [MaxLength(12)]
    [Required]
    [Display(Name = "Category Name")]
    public string CategoryName { get; set; }
    public int CategoryClothesCollectionCount { get; set; }
    public string Url => $"/c/{this.CategoryName.Replace(" ", "-").Trim('-')}";
}
