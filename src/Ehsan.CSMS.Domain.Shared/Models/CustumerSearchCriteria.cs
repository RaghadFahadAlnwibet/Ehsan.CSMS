using Ehsan.CSMS.Constant;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ehsan.CSMS.Models;

public class CustumerSearchCriteria
{
    [Display(Name = CostumerFields.CustomerId)]
    public Guid? Id { get; set; }
    [Display(Name = CostumerFields.CustomerName)]
    public string? Name { get; set; }
    [Display(Name = CostumerFields.MobileNumber)]
    public string? MobileNumber { get; set; }
}
