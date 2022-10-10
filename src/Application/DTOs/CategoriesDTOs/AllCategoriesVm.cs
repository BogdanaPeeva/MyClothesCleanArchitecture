using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClothesCA.Application.DTOs.CategoriesDTOs;
public class AllCategoriesVm
{
    public ICollection<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
}
