using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ehsan.CSMS.Entities;
/// <summary>
/// Represent a domain model for invoice entity (POCO Class)
/// </summary>
public class Invoice : CreationAuditedEntity<Guid>
{
    public Guid OrderId { get; set; }
    // float
    public double NetPrice { get; set; }
    public double Discount { get; set; }
    // Virtual navigation property for lazy loading
    public virtual Order? Order { get; set; }
}
