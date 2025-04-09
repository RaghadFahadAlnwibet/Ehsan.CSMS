using Ehsan.CSMS.Dtos.OrderDetailsDto;
using Ehsan.CSMS.Entities;
using Ehsan.CSMS.IRepositories;
using Ehsan.CSMS.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Ehsan.CSMS.Services;

public class OrderDetailsService : ApplicationService, IOrderDetailsService
{
    private readonly IOrderDetailsRepository _orderDetailRepository;
    public OrderDetailsService(IOrderDetailsRepository orderRepository)
    {
        _orderDetailRepository = orderRepository;
    }


    public async Task<IEnumerable<OrderDetailsResponse>> GetByIdAsync(Guid? id)
    {
        if (id is null)
        { throw new ArgumentNullException(nameof(id)); }
        var orderDetailsEntity = await _orderDetailRepository.GetByIdAsync(id.Value);
        return ObjectMapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailsResponse>>(orderDetailsEntity);
    }
}
