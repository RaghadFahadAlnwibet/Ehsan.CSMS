using Ehsan.CSMS.Constant;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Ehsan.CSMS.Dtos.CategoryDto;
/// <summary>
/// Represent response dto for category entity
/// </summary>
public class CategoryResponse : CreationAuditedEntityDto<Guid>
{
    /// <summary>
    /// Category name of the category response object  
    /// </summary>
    [Display(Name = CategoryFields.CategoryName)]
    public string? Name { get; set; }
}
