using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Ehsan.CSMS.Constant;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ehsan.CSMS.Dtos
{
    public class CostumerDto: EntityDto<int>
    {
        [Required(ErrorMessage = "Please enter Customer name.")]
        [MaxLength(100)]
        [Display(Name = Fields.CostumerFields.CustomerName)]
        public required string CustomerName { get; set; }

        [Required(ErrorMessage = "Please enter Mobile Number.")]
        [MaxLength(10)]
        [Display(Name = Fields.CostumerFields.ContactInfo)]

        public required string ContactInfo { get; set; }

        [Display(Name = Fields.CostumerFields.LoyaltyPoints)]
        public int LoyaltyPoints { get; set; } = 0;

        //public int? discount => (LoyaltyPoints * 2);



    }
   
}
