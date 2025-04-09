using Ehsan.CSMS.Constant;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ehsan.CSMS.Models;

public class CategorySearchCriteria
{
    [Display(Name = CategoryFields.CategoryId)]
    public Guid? Id { get; set; }
    [Display(Name = CategoryFields.CategoryName)]
    public string? Name { get; set; }
}