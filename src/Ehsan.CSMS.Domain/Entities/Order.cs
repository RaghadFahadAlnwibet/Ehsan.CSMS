using Ehsan.CSMS.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ehsan.CSMS.Entities;
/// <summary>
/// Represent a domain model for order entity (POCO Class)
/// </summary>
public class Order : CreationAuditedEntity<Guid>
{
    [Required]
    public Guid CashierId { get; set; }
    [Required]
    public Guid CustomerId { get; set; }
    // int in sql 
    public OrderStatus OrderStatus { get; set; }
    public double TotalPrice { get; set; }
    // Virtual navigation property for lazy loading
    public virtual Cashier? Cashier { get; set; }
    // Virtual navigation property for lazy loading
    public virtual Customer? Customer { get; set; }
    // Virtual navigation property for lazy loading
    public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    // Virtual navigation property for lazy loading
    public virtual Invoice? Invoice { get; set; }
}
