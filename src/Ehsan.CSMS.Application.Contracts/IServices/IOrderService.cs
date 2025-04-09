using Ehsan.CSMS.Dtos.OrderDto;
using Ehsan.CSMS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ehsan.CSMS.IService;
/// <summary>
/// Order services contract
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Add an order request object to the data store
    /// </summary>
    /// <param name="order">order request object to add</param>
    /// <returns>Returns order request object</returns>
    Task<OrderResponse> AddAsync(OrderAddRequest? order);
    /// <summary>
    /// Delete an order request object based on the given id from the data store
    /// </summary>
    /// <param name="id">order request object to delete</param>
    /// <returns>Returns true if successful delete, otherwise false</returns>
    Task<bool> DeleteByIdAsync(Guid? id);
    /// <summary>
    /// Update an order request object in the data store
    /// </summary>
    /// <param name="order">order request object to update</param>
    /// <returns>Returns order request object after updating</returns>
    Task<OrderResponse> UpdateAsync(OrderUpdateRequest? order);
    /// <summary>
    /// Get all order request objects from the data store 
    /// </summary>
    /// <returns>Returns all order request objects</returns>
    Task<IEnumerable<OrderResponse>> GetAllAsync();
    /// <summary>
    /// Get an order request object based on the given id from the data store
    /// </summary>
    /// <param name="id">Order request object id to search</param>
    /// <returns>Returns order request object based on the given id if exit, otherwise null</returns>
    Task<OrderResponse?> GetByIdAsync(Guid? id);
    /// <summary>
    /// Get all order request objects from the data store based on the given search criteria
    /// </summary>
    /// <param name="searchCriteria">search criteria (id, status, ..) to search </param>
    /// <returns>Returns all order request objects based on the given search criteria exit,
    /// otherwise all orders </returns>
    Task<IEnumerable<OrderResponse>> SearchOrderAsync(OrderSearchCriteria searchCriteria);
}
