using Ehsan.CSMS.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ehsan.CSMS.IService
{
    public interface IOrderDeatilsService
    {
        //OrderDeatilsDto
        Task AddAsync(OrderDeatilsDto orderDetail);

        Task AddManyAsync(IEnumerable<OrderDeatilsDto> orderDetails);
        Task DeleteAsync(OrderDeatilsDto orderDetail);
        Task DeletebyIdAsync(int id);
        Task UpdateAync(OrderDeatilsDto orderDetail);
        Task<List<OrderDeatilsDto>> GetAllAsync();
        Task<OrderDeatilsDto> GetbyIdAsync(int id);
        Task<int> GetNoOfItemsByOrderId(int id);

        Task<decimal> GetTotalPricebyIdAsync(int id);

    }
}
