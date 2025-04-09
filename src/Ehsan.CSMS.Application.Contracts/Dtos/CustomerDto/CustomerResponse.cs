using Ehsan.CSMS.Constant;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Ehsan.CSMS.Dtos.CustomerDto;
/// <summary>
/// Represent response dto for customer entity
/// </summary>

public class CustomerResponse : CreationAuditedEntityDto<Guid>
{
    /// <summary>
    /// Customer name of the customer response object 
    /// </summary>
    [Display(Name = CostumerFields.CustomerName)]
    public string? Name { get; set; }
    /// <summary>
    /// Mobile number of the customer response object
    /// </summary>
    [Display(Name = CostumerFields.MobileNumber)]
    public string? MobileNumber { get; set; }
    /// <summary>
    /// Loyalty point of the customer response object
    /// </summary>
    [Display(Name = CostumerFields.LoyaltyPoint)]
    public int LoyaltyPoint { get; set; }
}

