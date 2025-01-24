using Ehsan.CSMS.Models;
using Ehsan.CSMS.Dtos;
using System.Collections.Generic;
using Ehsan.CSMS.Enums;

namespace Ehsan.CSMS.Web.Models
{
    public class ProductViewModel
    {
        public ProductSearchCriteria productSearchCriteria { get; set; }
        public ProductDto product { get; set; }
        public List<ProductDto> products { get; set; }

    }
}
