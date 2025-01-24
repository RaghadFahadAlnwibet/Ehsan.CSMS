using Ehsan.CSMS.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ehsan.CSMS.Models
{
    public class CategorySearchCriteria
    {
        public int? Id { get; set; }
        [Display(Name = Fields.CategoryFields.CategoryName)]
        public String CategoryName { get; set; }
    }
}
