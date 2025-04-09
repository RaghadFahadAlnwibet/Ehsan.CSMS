using Ehsan.CSMS.Constant;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ehsan.CSMS.Models;

public class CashierSearchCriteria
{
    [Display(Name = CashierFields.CashierId)]
    public Guid? Id { get; set; }

    [Display(Name = CashierFields.CashierName)]
    public string? Name { get; set; }
}
