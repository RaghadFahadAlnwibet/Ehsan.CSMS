using Ehsan.CSMS.Dtos;
using Ehsan.CSMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ehsan.CSMS.IService
{
    public interface IOrderService
    {
        Task AddAsync(OrderDto order);
        Task DeleteAsync(OrderDto order);
        Task DeleteByIdAsync(int id);
        Task UpdateAsync(OrderDto order);
        Task<List<OrderDto>> GetAllAsync();
        Task<OrderDto> GetByIdAsync(int id);
        Task<List<OrderDto>> SearchOrderAsync(OrderSearchCriteria criteria);
        Task<List<OrderDto>> SerachByOrderStatusAsync(int status);
        Task<double> GetTotalPriceByIdAsync(int id);
    }
}
