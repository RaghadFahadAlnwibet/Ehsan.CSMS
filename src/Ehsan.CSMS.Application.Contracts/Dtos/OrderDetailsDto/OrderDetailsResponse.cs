using Ehsan.CSMS.Dtos.OrderDto;
using Ehsan.CSMS.Dtos.ProductDto;
using System;
using Volo.Abp.Application.Dtos;

namespace Ehsan.CSMS.Dtos.OrderDetailsDto;
/// <summary>
/// Represent response dto for order details entity
/// </summary>
public class OrderDetailsResponse : AuditedEntityDto<Guid>
{
    /// <summary>
    /// Product id of the order details response object  
    /// </summary>
    public Guid ProductId { get; set; }
    /// <summary>
    /// Number of products of the order details response object 
    /// </summary>
    public int Quantity { get; set; }
    /// <summary>
    /// Product price 
    /// </summary>
    public double ProductPrice { get; set; }
    /// <summary>
    /// Total price for the product: Quantity * ProductPrice 
    /// of the order details response object 
    /// </summary>
    public double TotalPrice { get; set; }

    /// <summary>
    /// Product navigation property of the order details response object 
    /// </summary>
    public ProductResponse? Product { get; set; }
    /// <summary>
    /// Order navigation property of the order details response object 
    /// </summary>
    public OrderResponse? Order { get; set; }
}
