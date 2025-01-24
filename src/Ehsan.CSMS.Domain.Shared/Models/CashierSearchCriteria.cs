using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ehsan.CSMS.Constant;

namespace Ehsan.CSMS.Models
{
    public class CashierSearchCriteria
    {
        public int? Id { get; set; }

        [Display(Name = Fields.CashierFields.CashierName)]
        public string? CashierName { get; set; }


    }
}
