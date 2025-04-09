using Ehsan.CSMS.Entities;
using Ehsan.CSMS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Ehsan.CSMS.IRepositories;
/// <summary>
/// Represents data access logic for customer entity   
/// </summary>

public interface ICustomerRepository : IRepository<Customer>
{
    /// <summary>
    /// Add customer entity object to the data store
    /// </summary>
    /// <param name="customer">customer entity object to add</param>
    /// <returns>Returns customer entity object</returns>
    Task<Customer> AddAsync(Customer customer);
    /// <summary>
    /// Delete customer entity object from the data store by id
    /// </summary>
    /// <param name="id">customer entity object id to delete</param>
    /// <returns>Returns true if success delete, otherwise null</returns>
    Task<bool> DeleteByIdAsync(Guid id);
    /// <summary>
    /// Update customer entity object in the data store
    /// </summary>
    /// <param name="customer">customer entity object to update</param>
    /// <returns>Returns customer entity object after updating</returns>
    Task<Customer> UpdateAsync(Customer customer);
    /// <summary>
    /// Get all customer entity objects from the data store 
    /// </summary>
    /// <returns>Returns all customer entity objects</returns>
    Task<List<Customer>> GetAllAsync();
    /// <summary>
    /// Get customer entity object from the data store by id
    /// </summary>
    /// <param name="id">customer entity object id</param>
    /// <returns>Returns customer entity object if exit, otherwise null</returns>
    Task<Customer?> GetByIdAsync(Guid id);
    /// <summary>
    /// Get customer entity object loyalty point by customer id
    /// </summary>
    /// <param name="id">customer entity object id to search</param>
    /// <returns>Returns customer entity object loyalty point </returns>
    Task<int> GetLoyaltyPointByIdAsync(Guid id);
    /// <summary>
    /// Get customer entity object from the data store by mobile number
    /// </summary>
    /// <param name="mobileNumber">customer entity object mobile number</param>
    /// <returns>Returns customer entity object if exit, otherwise null</returns>
    Task<Customer?> GetByMobileNumberAsync(string mobileNumber);
    /// <summary>
    /// Get all customers entity objects from the data store by search criteria
    /// </summary>
    /// <param name="searchCriteria">search criteria (id, name,..) to search</param>
    /// <returns>Returns all customers entity objects based on the search criteria if exit, 
    /// otherwise all customers</returns>
    Task<List<Customer>> SearchAsync(CustumerSearchCriteria searchCriteria);
}
