using Ehsan.CSMS.Models;
using System.Collections.Generic;
using Ehsan.CSMS.Dtos.CustomerDto;

namespace Ehsan.CSMS.Web.Models.CustomerViewModel;

public class CostumerViewModel
{
    public CustumerSearchCriteria? costumerSearchCriteria { get; set; }
    public IEnumerable<CustomerResponse>? costumers { get; set; }
}
