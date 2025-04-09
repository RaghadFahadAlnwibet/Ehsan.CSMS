using Ehsan.CSMS.Dtos.CustomerDto;
using Ehsan.CSMS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ehsan.CSMS.IService;
/// <summary>
/// Customer services contract
/// </summary>
public interface ICustomerService
{
    /// <summary>
    /// Add a new customer request object to the data store
    /// </summary>
    /// <param name="customer">Returns customer request object</param>
    /// <returns>Returns customer request object</returns>
    Task<CustomerResponse> AddAsync(CustomerAddRequest? customer);
    /// <summary>
    /// Delete a customer request object based on the given id from the data store
    /// </summary>
    /// <param name="id">customer request object id to delete</param>
    /// <returns>Returns true if successful delete, otherwise false</returns>
    Task<bool> DeleteByIdAsync(Guid? id);
    /// <summary>
    /// Update a customer request object in the data store
    /// </summary>
    /// <param name="customer">customer request object to update</param>
    /// <returns>Returns customer request object after updating</returns>
    Task<CustomerResponse> UpdateAsync(CustomerUpdateRequest? customer);
    /// <summary>
    /// Get all customer request objects from the data store
    /// </summary>
    /// <returns>Returns all customer request objects</returns>
    Task<IEnumerable<CustomerResponse>> GetAllAsync();
    /// <summary>
    /// Get a customer request object based on the given id from the data store
    /// </summary>
    /// <param name="id">customer request object id to search</param>
    /// <returns>Returns customer request object based on the given id if exit, otherwise null</returns>
    Task<CustomerResponse?> GetByIdAsync(Guid? id);
    /// <summary>
    /// Get customer request object loyalty point by customer id
    /// </summary>
    /// <param name="id">customer request object loyalty point</param>
    /// <returns>Returns customer request object loyalty point </returns>
    Task<int> GetLoyaltyPointByIdAsync(Guid? id);
    /// <summary>
    /// Get customer request object by customer mobile
    /// </summary>
    /// <param name="id">customer request object mobile</param>
    /// <returns>Returns customer request object mobile </returns>
    Task<CustomerResponse?> GetByMobile(string? mobile);

    /// <summary>
    /// Get all customer request objects from the data store based on the given search criteria
    /// </summary>
    /// <param name="searchCriteria">customer request object (id, name,..) to search</param>
    /// <returns>Returns all customer request objects based on the given search criteria if exit,
    /// otherwise all customers</returns>
    Task<IEnumerable<CustomerResponse>> SearchAsync(CustumerSearchCriteria searchCriteria);
}
