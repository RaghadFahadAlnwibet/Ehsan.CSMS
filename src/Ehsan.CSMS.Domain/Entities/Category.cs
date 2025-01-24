using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
// add-migration addcategorytable: After adding all tabl

namespace Ehsan.CSMS.Entities
{
    public class Category:Entity<int>
    {
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
