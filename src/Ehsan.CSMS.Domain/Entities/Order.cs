using Ehsan.CSMS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;


namespace Ehsan.CSMS.Entities
{
    public class Order : AuditedEntity<int>
    {
        public int CashierId { get; set; }
        public int CustomerId { get; set; }
        public OrderStatus OrderStatus { get; set; } // null means no order created
        public double NetPrice { get; set; }
        public double discount { get; set; }
        public double TotalPrice { get; set; }


        public virtual Cashier Cashier { get; set; } = null!;
        [JsonIgnore]
        public  Customer Customer { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    }
}
