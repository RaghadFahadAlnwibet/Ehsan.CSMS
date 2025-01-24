using Ehsan.CSMS.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ehsan.CSMS.Models
{
    public class OrderSearchCriteria // serach by order id or cashier name
    {
        [Display(Name = Fields.OrderFields.OrderId)]
        public int? OrderId { get; set; }

        [Display(Name = Fields.CashierFields.CashierName)]
        public int? CashierId { get; set; }

        //public int orderStatus { get; set; }
    }
}
