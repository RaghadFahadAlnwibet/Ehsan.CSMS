using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Ehsan.CSMS.Constant;
using System.ComponentModel.DataAnnotations;
using Ehsan.CSMS.Enums;
using System.Text.Json.Serialization;

namespace Ehsan.CSMS.Dtos
{
    public class OrderDeatilsDto: EntityDto<int>
    {
        
        public  int ProductId { get; set; }
        public  int OrderId { get; set; }

        [Display(Name = Fields.OrderDetailFields.TotalPrice)]

        [Required(ErrorMessage = "Total price must be set.")]
        public  double TotalPrice { get; set; }

        [Display(Name = Fields.OrderDetailFields.PricePerUnit)]
        public  decimal PricePerUnit { get; set; }

        [Required(ErrorMessage = "Quantity must be between 1 and 8.")]
        [Range(1,8)]
        [Display(Name = Fields.OrderDetailFields.Quantity)]
        public  int Quantity { get; set; }

        public virtual ProductDto Product { get; set; }
        [JsonIgnore]
        public virtual OrderDto order { get; set; }


    }
}
