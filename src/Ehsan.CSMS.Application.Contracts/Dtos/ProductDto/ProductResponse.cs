using Ehsan.CSMS.Constant;
using Ehsan.CSMS.Dtos.CategoryDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Ehsan.CSMS.Dtos.ProductDto;
/// <summary>
/// Represent response dto for product entity 
/// </summary>

public class ProductResponse : CreationAuditedEntityDto<Guid>
{
    /// <summary>
    /// Category name of the product response object
    /// </summary>
    [Display(Name = CategoryFields.CategoryName)]
    public Guid CategoryId { get; set; }
    /// <summary>
    /// Product name of the product response object
    /// </summary>
    [Display(Name = ProductFields.ProductName)]
    public string? Name { get; set; }
    /// <summary>
    /// Product price of the product response object
    /// </summary>
    [Display(Name = ProductFields.ProductPrice)]
    public double Price { get; set; }

    /// <summary>
    /// Category navigation of the product response object
    /// </summary>
    public CategoryResponse? Category { get; set; }
}
