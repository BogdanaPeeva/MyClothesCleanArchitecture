using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClothesCA.Application.DTOs.CategoriesDTOs;
public class CategoryDto
{
    public string CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int CategoryClothesCollectionCount { get; set; }
    public string ImageUrl { get; set; }
    public string Url => $"/c/{CategoryName.Replace(" ", "-").Trim('-')}";
}
