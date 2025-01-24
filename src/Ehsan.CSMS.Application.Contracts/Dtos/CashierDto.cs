using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Ehsan.CSMS.Constant;
using System.Collections;

namespace Ehsan.CSMS.Dtos
{
    public class CashierDto: EntityDto<int>
    {
        
        [Display(Name = Fields.CashierFields.CashierName)]
        [MaxLength(10)]
        [Required(ErrorMessage = "Please enter Cashier name.")]
        public required String CashierName { get; set; }


    }  
}
