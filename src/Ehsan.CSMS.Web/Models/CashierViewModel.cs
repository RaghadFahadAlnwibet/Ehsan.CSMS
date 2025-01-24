using Ehsan.CSMS.Dtos;
using Ehsan.CSMS.Models;
using System.Collections.Generic;

namespace Ehsan.CSMS.Web.Models
{
    public class CashierViewModel
    {
      public CashierSearchCriteria? SearchCriteria { get; set; }
      public  List<CashierDto>? Cashiers { get; set; }
    }
}
