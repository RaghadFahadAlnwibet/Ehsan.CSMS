using Ehsan.CSMS.Constant;
using Ehsan.CSMS.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ehsan.CSMS.Models;

public class OrderSearchCriteria
{
    [Display(Name = OrderFields.OrderId)]
    public Guid? Id { get; set; }
    [Display(Name = CashierFields.CashierName)]
    public Guid? CashierId { get; set; }
    [Display(Name = OrderFields.OrderStatus)]
    public OrderStatus OrderStatus { get; set; }
}
