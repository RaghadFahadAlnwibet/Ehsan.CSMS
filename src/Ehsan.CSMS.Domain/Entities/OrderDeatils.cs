using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ehsan.CSMS.Entities;
/// <summary>
/// Represent a domain model for orderDetail entity (POCO Class).
/// </summary>
public class OrderDetail : CreationAuditedEntity<Guid>
{
    [Required]
    public Guid ProductID { get; set; }
    [Required]
    public Guid OrderId { get; set; }
    [Required]
    public double TotalPrice { get; set; }
    public double ProductPrice { get; set; }
    [Required]
    public int Quantity { get; set; }
    // Virtual navigation property for lazy loading
    public virtual Order? Order { get; set; }
    // Virtual navigation property for lazy loading
    public virtual Product? Product { get; set; }
}
