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
    public class OrderDto: AuditedEntityDto<int>
    {// Order_Date not null 
        [Display(Name = Fields.CashierFields.CashierName)]
        public  int CashierId { get; set; }

        [Display(Name = Fields.CostumerFields.CustomerName)]

        public  int CustomerId { get; set; }

        [Display(Name = Fields.OrderFields.OrderStatus)]
        public OrderStatus OrderStatus { get; set; }

        [Required(ErrorMessage = "Total Price is required.")]

        [Display(Name = Fields.OrderFields.TotalPrice)]
        public  double TotalPrice { get; set; }

        [Display(Name = Fields.OrderFields.NetPrice)]

        public  double NetPrice { get; set; }

        [Display(Name = Fields.OrderFields.discount)]

        public  double discount { get; set; }

        [Display(Name = Fields.CashierFields.CashierName)]
        public virtual CashierDto Cashier { get; set; } = null!;

        [Display(Name = Fields.CostumerFields.CustomerName)]
        public virtual CostumerDto Customer { get; set; } = null!;

        public virtual List<OrderDeatilsDto> OrderDetails { get; set; } = new List<OrderDeatilsDto>();
    }
}

