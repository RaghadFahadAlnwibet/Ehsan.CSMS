using Ehsan.CSMS.Dtos.OrderDto;
using Ehsan.CSMS.Dtos.ProductDto;
using System;
using System.Collections.Generic;

namespace Ehsan.CSMS.Web.Models.OrderViewModels;

public class UpdateOrderViewMode
{
    public List<Guid>? SelectedProduct { get; set; }
    public List<decimal>? PricePerUnit { get; set; }

    public List<int>? Quantity { get; set; }
    public List<int>? SubTotals { get; set; }

    public OrderUpdateRequest? orderUpdateRequest { get; set; }
    //public OrderDetailsAddRequest OrderDetails { get; set; }
    // TomList populated using ajax 
    public IEnumerable<ProductResponse>? Products { get; set; }

}
