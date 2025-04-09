using Ehsan.CSMS.Constant;
using System.ComponentModel.DataAnnotations;

namespace Ehsan.CSMS.Dtos.CategoryDto;
/// <summary>
/// Represent add request dto for category entity
/// </summary>
public class CategoryAddRequest
{
    /// <summary>
    /// Category name of the category request object 
    /// </summary>
    [Display(Name = CategoryFields.CategoryName)]
    [Required(ErrorMessage = "Please enter {0}.")]
    [RegularExpression("^[a-zA-z ]*$", ErrorMessage = "{0} can only contain alphabetic characters and spaces.")]
    [StringLength(40, ErrorMessage = "{0} can't be longer than {1} characters.")]
    [MaxLength(40, ErrorMessage = "{0} can't be longer than {1} characters.")]

    public string? Name { get; set; }
}
