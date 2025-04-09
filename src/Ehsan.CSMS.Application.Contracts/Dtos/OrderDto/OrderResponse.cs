using Ehsan.CSMS.Constant;
using Ehsan.CSMS.Dtos.CashierDto;
using Ehsan.CSMS.Dtos.CustomerDto;
using Ehsan.CSMS.Dtos.InvoiceDto;
using Ehsan.CSMS.Dtos.OrderDetailsDto;
using Ehsan.CSMS.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Ehsan.CSMS.Dtos.OrderDto;
/// <summary>
/// Represent response dto for order entity
/// </summary>
public class OrderResponse : AuditedEntityDto<Guid>
{
    /// <summary>
    /// Invoice id of the order response object
    /// </summary>
    public Guid InvoiceId { get; set; }
    /// <summary>
    /// Order status of the order response object
    /// </summary>
    public OrderStatus OrderStatus { get; set; }
    /// <summary>
    /// Total price of the order response object
    /// </summary>
    public double TotalPrice { get; set; }
    /// <summary>
    /// Customer navigation of the order response object
    /// </summary>
    [Display(Name = CostumerFields.CustomerName)]
    public CustomerResponse? Customer { get; set; }
    /// <summary>
    /// Cashier navigation of the order response object
    /// </summary>
    [Display(Name = CashierFields.CashierName)]
    public CashierResponse? Cashier { get; set; }
    /// <summary>
    /// OrderDetails navigation of the order response object
    /// </summary>
    public List<OrderDetailsResponse>? OrderDetailsResponses { get; set; }
    /// <summary>
    /// Invoice navigation of the order response object
    /// </summary>
    public InvoiceResponse? Invoice { get; set; }
}
