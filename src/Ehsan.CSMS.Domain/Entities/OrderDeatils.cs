using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Ehsan.CSMS.Entities
{
    public class OrderDetail : Entity<int>
    {
        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal PricePerUnit { get; set; }

        public int Quantity { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;

    }
}
