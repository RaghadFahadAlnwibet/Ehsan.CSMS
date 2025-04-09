using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
namespace Ehsan.CSMS.Entities;
/// <summary>
/// Represent a domain model for cashier entity (POCO Class)
/// </summary>
public class Cashier : CreationAuditedEntity<Guid>
{
    [Required]
    [StringLength(40)]
    public string? Name { get; set; }

    // Virtual navigation property for lazy loading
    public virtual ICollection<Order>? Orders { get; set; }

}
