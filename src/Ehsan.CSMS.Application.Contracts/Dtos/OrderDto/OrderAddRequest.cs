using Ehsan.CSMS.Constant;
using Ehsan.CSMS.Dtos.CustomerDto;
using Ehsan.CSMS.Dtos.InvoiceDto;
using Ehsan.CSMS.Dtos.OrderDetailsDto;
using Ehsan.CSMS.Dtos.ProductDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ehsan.CSMS.Dtos.OrderDto;
/// <summary>
/// Represent add request dto for order entity
/// </summary>
public class OrderAddRequest
{
    /// <summary>
    /// Cashier id of the order request object
    [Display(Name = CashierFields.CashierName)]
    [Required(ErrorMessage = "Please select {0}")]
    public Guid CashierId { get; set; }
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Customer of the order request object
    /// </summary>
    public CustomerUpdateRequest? Customer { get; set; }
   
    [MinLength(1, ErrorMessage = "Please select at lease 1 product to make an order")]
    [Display(Name = OrderDetailsFields.orderDetailId)]
    /// <summary>
    /// Order details dto add request for the order request object
    /// </summary>
    public List<OrderDetailsAddRequest>? OrderDetailsRequests { get; set; }

    public InvoiceResponse? Invoice { get; set; }

}

