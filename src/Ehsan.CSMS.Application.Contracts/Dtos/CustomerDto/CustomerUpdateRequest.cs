using Ehsan.CSMS.Constant;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Ehsan.CSMS.Dtos.CustomerDto;
/// <summary>
/// Represent update request dto for customer entity 
/// </summary>
public class CustomerUpdateRequest : AuditedEntityDto<Guid>
{
    /// <summary>
    /// Customer name of the customer request object
    /// </summary>
    [Display(Name = CostumerFields.CustomerName)]
    [Required(ErrorMessage = "Please enter {0}.")]
    [RegularExpression("^[a-zA-Z ]*$",
    ErrorMessage = "{0} can only contain alphabetic characters and spaces.")]
    [StringLength(40, ErrorMessage = "{0} can't be longer than {1} characters.")]
    [MaxLength(40, ErrorMessage = " {0} can't be longer than {1} characters.")]
    public string? Name { get; set; }
    /// <summary>
    /// Mobile number of the customer request object
    /// </summary>
    [Display(Name = CostumerFields.MobileNumber)]
    [Phone]
    [RegularExpression("^[0-9]{10}$",
    ErrorMessage = "{0} must be exactly 10 digits long.")]
    [Required(ErrorMessage = "Please enter {0}.")]
    public string? MobileNumber { get; set; }
    /// <summary>
    /// Loyalty point of the customer request object
    /// </summary>
    [Display(Name = CostumerFields.LoyaltyPoint)]
    public int LoyaltyPoint { get; set; }
}
