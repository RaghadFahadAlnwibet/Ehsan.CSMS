using Ehsan.CSMS.Entities;
using Ehsan.CSMS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Ehsan.CSMS.IRepositories;
/// <summary>
/// Represents data access logic for order entity
/// </summary>
public interface IOrderRepository : IRepository<Order>
{
    /// <summary>
    /// Add a new order entity object to the data store
    /// </summary>
    /// <param name="order">order entity object to add</param>
    /// <returns>Returns order entity object</returns>
    Task<Order> AddAsync(Order order);
    /// <summary>
    /// Delete an order entity object from the data store by id
    /// </summary>
    /// <param name="id">order entity object id to delete</param>
    /// <returns>Returns true if success delete, otherwise null/returns>
    Task<bool> DeleteByIdAsync(Guid id);
    /// <summary>
    /// Update an order entity object in the data store
    /// </summary>
    /// <param name="order">order entity object to update</param>
    /// <returns>order entity object after updating</returns>
    Task<Order> UpdateAsync(Order order);
    /// <summary>
    /// Get all order entity objects from the data store
    /// </summary>
    /// <returns>Returns all order entity objects</returns>
    Task<List<Order>> GetAllAsync();
    /// <summary>
    /// Get an order entity object from the data store by id
    /// </summary>
    /// <param name="id">order entity object to search</param>
    /// <returns>Returns order entity object if exit, otherwise null</returns>
    Task<Order?> GetByIdAsync(Guid id);
    /// <summary>
    /// Search order entity objects from the data store by search criteria
    /// </summary>
    /// <param name="searchCriteria">search criteria (id, name, ..) to search</param>
    /// <returns>Returns all order entity objects based on the search criteria if exit, 
    /// otherwise all order</returns>
    Task<List<Order>> SearchOrderAsync(OrderSearchCriteria searchCriteria);
}
