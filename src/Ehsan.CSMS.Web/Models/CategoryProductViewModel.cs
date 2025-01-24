using Ehsan.CSMS.Dtos;
using System.Collections.Generic;

namespace Ehsan.CSMS.Web.Models
{
    public class CategoryProductViewModel
    {
        public List<ProductDto> product { get; set; }
        public CategoryDto category { get; set; }
    }
}
