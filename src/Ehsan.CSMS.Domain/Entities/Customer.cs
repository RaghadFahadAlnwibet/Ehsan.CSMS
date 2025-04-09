using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ehsan.CSMS.Entities;
/// <summary>
/// Represent a domain model for customer entity (POCO Class)
/// </summary>
public class Customer : CreationAuditedEntity<Guid>
{
    [Required]
    [StringLength(40)]
    public string? Name { get; set; }
    [StringLength(10)]
    [Required]
    public string? MobileNumber { get; set; }
    public int? LoyaltyPoint { get; set; }
    // Virtual navigation property for lazy loading
    public virtual ICollection<Order>? Orders { get; set; }
}
