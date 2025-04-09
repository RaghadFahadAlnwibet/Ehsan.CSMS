using Ehsan.CSMS.Dtos.CustomerDto;
using Ehsan.CSMS.Dtos.OrderDetailsDto;
using Ehsan.CSMS.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Ehsan.CSMS.Dtos.OrderDto;
/// <summary>
/// Represet update request dto for order entity 
/// </summary>
public class OrderUpdateRequest : AuditedEntityDto<Guid>
{
    /// <summary>
    /// Order status of the order request object  
    /// </summary>
    
    public OrderStatus orderStatus { get; set; }

    public List<OrderDetailsUpdateRequest>? OrderDetailsUpdateRequest { get; set; }

    //public CustomerResponse? Customer { get; set; }
}
