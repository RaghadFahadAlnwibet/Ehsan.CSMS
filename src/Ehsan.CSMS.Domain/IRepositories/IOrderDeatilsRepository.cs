using Ehsan.CSMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Ehsan.CSMS.IRepositories
{
     public interface IOrderDeatilsRepository: IRepository<OrderDetail>
    {
        Task AddAsync(OrderDetail orderDetail);
        Task AddManyAsync(IEnumerable<OrderDetail> orderDetails);
        Task DeleteAsync(OrderDetail orderDetail);
        Task DeletebyIdAsync(int id);
        Task UpdateAync(OrderDetail orderDetail);
        Task<List<OrderDetail>> GetAllAsync();
        Task<OrderDetail> GetbyIdAsync(int id);
        Task<int> GetNoOfItemsByOrderId(int id);

        Task<decimal> GetTotalPricebyIdAsync(int id); // GetTotalPricebyIdAsync * ( guantity from the view )


    }
}
