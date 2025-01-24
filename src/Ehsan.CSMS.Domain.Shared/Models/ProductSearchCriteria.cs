using Ehsan.CSMS.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ehsan.CSMS.Constant.Fields;

namespace Ehsan.CSMS.Models
{
    public class ProductSearchCriteria
    {
        [Display(Name = "Products")]

        public int? Id { get;  }
        [Display(Name = Fields.ProductFields.ProductName)]

        public String ProductName { get; set; }

        [DisplayName("Categories")]
        public int? CategoryId { get; set; }
    }
}
