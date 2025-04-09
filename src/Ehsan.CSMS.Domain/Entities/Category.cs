using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
namespace Ehsan.CSMS.Entities;
/// <summary>
/// Represents a domain model for category entity (POCO Class).
/// </summary>
public class Category : CreationAuditedEntity<Guid>
{
    [Required]
    [StringLength(40)]
    public string? Name { get; set; }
    // Virtual navigation property for lazy loading
    public virtual ICollection<Product>? Products { get; set; }
}
