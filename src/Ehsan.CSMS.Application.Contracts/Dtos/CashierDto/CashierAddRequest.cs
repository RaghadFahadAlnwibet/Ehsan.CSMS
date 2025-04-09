using Ehsan.CSMS.Constant;
using System.ComponentModel.DataAnnotations;

namespace Ehsan.CSMS.Dtos.CashierDto;
/// <summary>
/// Represent add request dto for cashier entity
/// </summary>
public class CashierAddRequest
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
