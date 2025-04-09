using Ehsan.CSMS.Constant;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ehsan.CSMS.Models;

public class ProductSearchCriteria
{
    [Display(Name = ProductFields.ProductId)]
    public Guid? Id { get; set; }
    [Display(Name = ProductFields.ProductName)]
    public string? Name { get; set; }
    [Display(Name = CategoryFields.CategoryName)]
    public Guid? CategoryId { get; set; }
}
