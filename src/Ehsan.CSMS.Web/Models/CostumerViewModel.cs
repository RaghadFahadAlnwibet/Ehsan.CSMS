using Ehsan.CSMS.Models;
using Ehsan.CSMS.Dtos;
using System.Collections.Generic;

namespace Ehsan.CSMS.Web.Models
{
    public class CostumerViewModel
    {
        public CostumerSearchCriteria costumerSearchCriteria { get; set; }
        public List<CostumerDto> costumers { get; set; }


    }
}
