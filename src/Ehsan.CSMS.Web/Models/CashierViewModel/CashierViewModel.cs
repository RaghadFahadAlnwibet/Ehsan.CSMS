using Ehsan.CSMS.Dtos.CashierDto;
using Ehsan.CSMS.Models;
using System.Collections.Generic;

namespace Ehsan.CSMS.Web.Models.CashierViewModel;

public class CashierViewModel
{
    public CashierSearchCriteria? SearchCriteria { get; set; }
    public IEnumerable<CashierResponse>? Cashiers { get; set; }
}
