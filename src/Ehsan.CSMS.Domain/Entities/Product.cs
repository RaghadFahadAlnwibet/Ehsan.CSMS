using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;


namespace Ehsan.CSMS.Entities
{
    public class Product : Entity<int>
    {
        public int CategoryId { get; set; }

        public string ProductName { get; set; } = null!;

        public double ProductPrice { get; set; }

        public  Category Category { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    }
}
