using Ehsan.CSMS.Models;
using Ehsan.CSMS.Dtos;
using System.Collections.Generic;
using Ehsan.CSMS.Enums;
using Ehsan.CSMS.Dtos.ProductDto;

namespace Ehsan.CSMS.Web.Models.ProductViewModel;

public class ProductViewModel
{
    public ProductSearchCriteria? ProductSearchCriteria { get; set; }
    public IEnumerable<ProductResponse>? Products { get; set; }
}
