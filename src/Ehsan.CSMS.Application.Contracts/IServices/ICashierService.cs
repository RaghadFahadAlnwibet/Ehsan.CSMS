using Ehsan.CSMS.Dtos.CashierDto;
using Ehsan.CSMS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ehsan.CSMS.IService;
/// <summary>
/// Cashier services contract  
/// </summary>
public interface ICashierService
{
    /// <summary>
    /// Add a new cashier request object to the data store
    /// </summary>
    /// <param name="cashier">cashier request object to add</param>
    /// <returns>Returns cashier request object</returns>
    Task<CashierResponse> AddAsync(CashierAddRequest? cashier);
    /// <summary>
    /// Delete a cashier request based on the given id from the data store
    /// </summary>
    /// <param name="id">cashier request id to delete</param>
    /// <returns>Returns true if successful delete, otherwise false</returns>
    Task<bool> DeleteByIdAsync(Guid? id);
    /// <summary>
    /// Update a cashier request object in the data store
    /// </summary>
    /// <param name="cashier">cashier request object to update</param>
    /// <returns>Returns cashier request object after updating</returns>
    Task<CashierResponse> UpdateAsync(CashierUpdateRequest? cashier);
    /// <summary>
    /// Get all cashier request objects from the data store
    /// </summary>
    /// <returns>Returns all cashier request objects</returns>
    Task<IEnumerable<CashierResponse>> GetAllAsync();
    /// <summary>
    /// Get a cashier request object based on the given id from the data store
    /// </summary>
    /// <param name="id">Cashier request id to search</param>
    /// <returns>Returns cashier request object based on the given id if exit, otherwise null</returns>
    Task<CashierResponse?> GetByIdAsync(Guid? id);
    /// <summary>
    /// Get all cashier request objects from the data store based on the given search criteria
    /// </summary>
    /// <param name="searchCriteria">search criteria (id, name).. to search </param>
    /// <returns>Returns all cashier request based on the given search criteria if exit, 
    /// otherwise all cashiers</returns>
    Task<IEnumerable<CashierResponse>> SearchAsync(CashierSearchCriteria searchCriteria);
}
