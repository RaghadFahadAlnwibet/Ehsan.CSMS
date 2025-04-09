using Ehsan.CSMS.Constant;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Ehsan.CSMS.Dtos.ProductDto;
/// <summary>
/// Represent update request dto for product entity 
/// </summary>
public class ProductUpdateRequest : AuditedEntityDto<Guid>
{
    /// <summary>
    /// Category name of the product request object
    /// </summary>
    [Required(ErrorMessage = "Please select {0}.")]
    [Display(Name = CategoryFields.CategoryName)]
    public Guid CategoryId { get; set; }
    /// <summary>
    /// Product name of the product request object
    /// </summary>

    [Display(Name = ProductFields.ProductName)]
    [Required(ErrorMessage = "Please enter {0}.")]
    [RegularExpression("^[a-zA-Z ]*$",
        ErrorMessage = "{0} can only contain alphabetic characters and spaces.")]
    [StringLength(40, ErrorMessage = "{0} can't be longer than {1} characters.")]
    [MaxLength(40, ErrorMessage = "{0} can't be longer than {1} characters.")]
    public string? Name { get; set; }
    /// <summary>
    /// Product price of the product request object
    /// </summary>

    [Required(ErrorMessage = "Please enter {0}.")]
    [Display(Name = ProductFields.ProductPrice)]
    public double Price { get; set; }
}
