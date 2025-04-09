using Ehsan.CSMS.Entities;
using Ehsan.CSMS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Ehsan.CSMS.IRepositories;
/// <summary>
/// Represents data access logic for cashier entity.    
/// </summary>
public interface ICashierRepository : IRepository<Cashier>
{
    /// <summary>
    /// Add a new cashier entity object to the data store
    /// </summary>
    /// <param name="cashier">cashier entity to add</param>
    /// <returns>Returns cashier entity</returns>
    Task<Cashier> AddAsync(Cashier cashier);
    /// <summary>
    /// Delete a cashier entity object from the data store by id
    /// </summary>
    /// <param name="id">cashier entity id to delete</param>
    /// <returns>Returns true if success delete, otherwise false</returns>
    Task<bool> DeleteByIdAsync(Guid id);
    /// <summary>
    /// Update a cashier entity object in the data store
    /// </summary>
    /// <param name="cashier">cashier entity object to update</param>
    /// <returns>Returns cashier entity object after updating</returns>
    Task<Cashier> UpdateAsync(Cashier cashier);
    /// <summary>
    /// Get all cashier entity objects from the data store
    /// </summary>
    /// <returns>Returns all cashier entity objects</returns>
    Task<List<Cashier>> GetAllAsync();
    /// <summary>
    /// Get a cashier entity object from the data store by id
    /// </summary>
    /// <param name="id">cashier entity object id to search</param>
    /// <returns>Returns cashier entity object if exit, otherwise null</returns>
    Task<Cashier?> GetByIdAsync(Guid id);
    /// <summary>
    /// Search cashier entity objects from the data store by search criteria
    /// </summary>
    /// <param name="searchCriteria">search criteria (id, name, ..) to search</param>
    /// <returns>Returns all cashier entity objects based on the search criteria if exit, 
    /// otherwise all cashiers</returns>
    Task<List<Cashier>> SearchAsync(CashierSearchCriteria searchCriteria);
}
