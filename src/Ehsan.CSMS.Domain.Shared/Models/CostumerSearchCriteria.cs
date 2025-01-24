using Ehsan.CSMS.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ehsan.CSMS.Models
{
    public class CostumerSearchCriteria
    {

        public int? Id { get; set; }

        [Display(Name = Fields.CostumerFields.CustomerName)]

        public String CustomerName { get; set; }

        [Display(Name = Fields.CostumerFields.ContactInfo)]

        public string mobileNumber { get; set; }
    }
}
