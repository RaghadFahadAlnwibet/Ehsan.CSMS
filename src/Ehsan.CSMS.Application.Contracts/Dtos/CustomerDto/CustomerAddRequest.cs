using Ehsan.CSMS.Constant;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Ehsan.CSMS.Dtos.CustomerDto;
/// <summary>
/// Represent add request dto for customer entity
/// </summary>
public class CustomerAddRequest
{
    /// <summary>
    /// Customer name of the customer request object
    /// </summary>
    [Display(Name = CostumerFields.CustomerName)]
    [Required(ErrorMessage = "Please enter {0}.")]
    [RegularExpression("^[a-zA-Z ]*$",
        ErrorMessage = "{0} can only contain alphabetic characters and spaces.")]
    [StringLength(40, ErrorMessage = "{0} can't be longer than {1} characters.")]
    [MaxLength(40, ErrorMessage = "{0} can't be longer than {1} characters.")]
    public string? Name { get; set; }
    /// <summary>
    /// Mobile number of the customer request object
    /// </summary>

    [Display(Name = CostumerFields.MobileNumber)]
    [RegularExpression("^[0-9]{10}$",
    ErrorMessage = "{0} must be exactly 10 digits long.")]
    [Phone]
    [Required(ErrorMessage = "Please enter {0}.")]
    [Remote(action: "IsExitMobileNumber", controller: "Customer", ErrorMessage = "Mobile Number is already exit")]
    public string? MobileNumber { get; set; }
    ///// <summary>
    ///// Loyalty point of the customer request object
    ///// </summary>
    //[Display(Name = CostumerFields.LoyaltyPoint)]
    //public int LoyaltyPoint { get; set; }
}
