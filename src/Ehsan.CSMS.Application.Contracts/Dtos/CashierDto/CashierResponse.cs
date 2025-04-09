using Ehsan.CSMS.Constant;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Ehsan.CSMS.Dtos.CashierDto;
/// <summary>
/// Represent response dto for cashier entity
/// </summary>
public class CashierResponse : CreationAuditedEntityDto<Guid>
{
    /// <summary>
    /// Cashier name of the cashier response object  
    /// </summary>
    [Display(Name = CashierFields.CashierName)]
    public string? Name { get; set; }
}
