using Ehsan.CSMS.Dtos.OrderDetailsDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ehsan.CSMS.IServices;
/// <summary>
/// OrderDetails service contract
/// </summary>
public interface IOrderDetailsService
{
    /// <summary>
    /// Get an order details request object based on the given id
    /// </summary>
    /// <param name="id">order details request object to search</param>
    /// <returns>Returns order details request object based on the given id if exit, 
    /// otherwise null</returns>
    Task<IEnumerable<OrderDetailsResponse>> GetByIdAsync(Guid? id);
    // future plan. this depends on the requirement of the project and the business logic
    // does the cashier can update the order details or not after placing it ? if yes add this functionality
}
