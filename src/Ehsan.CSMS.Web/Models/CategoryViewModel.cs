using Ehsan.CSMS.Models;
using Ehsan.CSMS.Dtos;
using System.Collections.Generic;
namespace Ehsan.CSMS.Web.Models
{
    public class CategoryViewModel
    {
        public CategorySearchCriteria? categorySearchCriteria { get; set; }
        public List<CategoryDto>? categories {  get; set; }
    }
}
