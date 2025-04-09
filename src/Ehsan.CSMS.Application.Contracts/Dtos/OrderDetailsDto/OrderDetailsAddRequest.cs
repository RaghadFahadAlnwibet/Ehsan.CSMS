using Ehsan.CSMS.Constant;
using Ehsan.CSMS.Dtos.ProductDto;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ehsan.CSMS.Dtos.OrderDetailsDto;
/// <summary>
/// Represent add request dto for order details entity
/// </summary>
public class OrderDetailsAddRequest
{
    /// <summary>
    /// Product id of the order details request object  
    /// </summary>
    [Display(Name = ProductFields.ProductName)]
    [Required(ErrorMessage = "Please select {0}")]
    public Guid ProductID { get; set; }
    /// <summary>
    /// Number of products of the order details request object
    /// </summary>
    [Display(Name = OrderDetailsFields.Quantity)]
    [Range(minimum: 1, maximum: int.MaxValue)]
    [Required(ErrorMessage = "Please enter {0}")]
    public int Quantity { get; set; }
}
