using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;


namespace Ehsan.CSMS.Entities
{    public class Cashier : Entity<int>
    {
        public string CashierName { get; set; } = null!;


        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
