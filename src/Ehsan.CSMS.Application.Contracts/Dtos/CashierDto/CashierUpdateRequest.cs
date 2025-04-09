using Ehsan.CSMS.Constant;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Ehsan.CSMS.Dtos.CashierDto;
/// <summary>
/// Represent update request dto for cashier entity
/// </summary>
public class CashierUpdateRequest : AuditedEntityDto<Guid>
{
    /// <summary>
    /// Cashier name of the cashier request object  
    /// </summary>
    [Display(Name = CashierFields.CashierName)]
    [Required(ErrorMessage = "Please enter {0}.")]
    [RegularExpression("^[a-zA-Z ]*$",
        ErrorMessage = "{0} can only contain alphabetic characters and spaces.")]
    [StringLength(40, ErrorMessage = "{0} can't be longer than {1} characters.")]
    [MaxLength(40, ErrorMessage = "{0} can't be longer than {1} characters.")]
    public string? Name { get; set; }

}
