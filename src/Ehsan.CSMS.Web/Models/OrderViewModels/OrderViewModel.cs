using Ehsan.CSMS.Dtos;
using Ehsan.CSMS.Dtos.OrderDto;
using Ehsan.CSMS.Entities;
using Ehsan.CSMS.Models;
using System.Collections.Generic;
namespace Ehsan.CSMS.Web.Models;

public class OrderViewModel
{
    public OrderSearchCriteria? OrderSearchCriteria { get; set; }
    public IEnumerable<OrderResponse>? orders { get; set; }
}




