using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;


namespace Ehsan.CSMS.Entities;
/// <summary>
/// Represent a domain model for product entity (POCO Class)
/// </summary>
public class Product : CreationAuditedEntity<Guid>
{
    [Required]
    public Guid CategoryId { get; set; }
    [Required]
    [StringLength(40)]
    public string? Name { get; set; }
    [Required]
    public double Price { get; set; }
    // Virtual navigation property for lazy loading
    public virtual Category? Category { get; set; }
    // Virtual navigation property for lazy loading
    public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
}