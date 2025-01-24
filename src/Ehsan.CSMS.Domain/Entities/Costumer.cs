using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Ehsan.CSMS.Entities
{
    public class Customer : Entity<int>
    {
        public string CustomerName { get; set; } = null!; 

        public required string ContactInfo { get; set; }

        public int LoyaltyPoints { get; set; } = 0;
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
