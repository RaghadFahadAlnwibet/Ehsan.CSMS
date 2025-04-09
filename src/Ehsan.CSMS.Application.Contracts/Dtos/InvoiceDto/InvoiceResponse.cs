using Ehsan.CSMS.Dtos.OrderDto;
using System;
using Volo.Abp.Application.Dtos;

namespace Ehsan.CSMS.Dtos.InvoiceDto;
/// <summary>
/// Represent response dto for invoice entity 
/// </summary>
public class InvoiceResponse : CreationAuditedEntityDto<Guid>
{
    /// <summary>
    /// Order id of the invoice
    /// </summary>
    public Guid OrderId { get; set; }
    /// <summary>
    /// Order navigation property of the invoice response object
    /// </summary>
    public double NetPrice { get; set; }
    public double Discount { get; set; }
    public double TotalPrice { get; set; }

    public OrderResponse? Order { get; set; }

}
