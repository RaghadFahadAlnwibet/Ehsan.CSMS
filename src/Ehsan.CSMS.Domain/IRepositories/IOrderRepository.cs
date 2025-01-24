using Ehsan.CSMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Ehsan.CSMS.Models;

namespace Ehsan.CSMS.IRepositories
{
    public interface IOrderRepository: IRepository<Order>
    {
        Task AddAsync(Order order);
        Task AddManyAsync(IEnumerable<Order> order);
        Task DeleteAsync(Order order);
        Task DeleteByIdAsync(int id);
        Task UpdateAsync(Order order);
        Task<List<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(int id);
        Task<List<Order>> SearchOrderAsync(OrderSearchCriteria criteria);
        Task<List<Order>> SerachByOrderStatusAsync(int status);
        Task<double> GetTotalPriceByIdAsync(int id); 
    }
}
