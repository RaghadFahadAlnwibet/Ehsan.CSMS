using Ehsan.CSMS.Constant;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Ehsan.CSMS.Dtos.CategoryDto;
/// <summary>
/// Represent update request dto for category entity
/// </summary>
public class CategoryUpdateRequest : AuditedEntityDto<Guid>
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
