using Ehsan.CSMS.Models;
using System.Collections.Generic;
using Ehsan.CSMS.Dtos.CategoryDto;
namespace Ehsan.CSMS.Web.Models.CategoryViewModels;

public class CategoryViewModel
{
    public CategorySearchCriteria? categorySearchCriteria { get; set; }
    public IEnumerable<CategoryResponse>? categories { get; set; }


}
