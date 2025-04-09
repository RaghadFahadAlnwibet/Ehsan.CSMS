using Ehsan.CSMS.Entities;
using Ehsan.CSMS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Ehsan.CSMS.IRepositories;
/// <summary>
/// Represents data access logic for product entity
/// </summary>
public interface IProductRepository : IRepository<Product>
{
    /// <summary>
    /// Add a new product entity object to the data store
    /// </summary>
    /// <param name="product">product entity object to add</param>
    /// <returns>Returns product entity object</returns>
    Task<Product> AddAsync(Product product);
    /// <summary>
    /// Delete a product entity object from the data store by id
    /// </summary>
    /// <param name="id">product entity object id to delete</param>
    /// <returns>Returns true if success delete, otherwise null</returns>
    Task<bool> DeleteByIdAsync(Guid id);
    /// <summary>
    /// Update a product entity object in the data store
    /// </summary>
    /// <param name="product">product entity object to update</param>
    /// <returns>Returns product entity object after updating</returns>
    Task<Product> UpdateAsync(Product product);
    /// <summary>
    /// Get all product entity objects from the data store
    /// </summary>
    /// <returns>Returns all product entity objects</returns>
    Task<List<Product>> GetAllAsync();
    /// <summary>
    /// Get a product entity object from the data store by id
    /// </summary>
    /// <param name="id">product entity object id to search</param>
    /// <returns>Returns product entity object if exit, otherwise null</returns>
    Task<Product?> GetByIdAsync(Guid id);
    /// <summary>
    /// Get a product entity object from the data store by name
    /// </summary>
    /// <param name="id">product entity object name to search</param>
    /// <returns>Returns product entity object if exit, 
    /// otherwise null</returns>
    Task<Product?> GetBynameAsync(string name);
    /// <summary>
    /// Get a product entity price from the data store by Id
    /// </summary>
    /// <param name="id">product entity price to search</param>
    /// <returns>Returns product entity price, 
    /// otherwise null</returns>
    Task<double?> GetPriceById(Guid Id);
    /// <summary>
    /// Search product entity objects from the data store by search criteria
    /// </summary>
    /// <param name="searchCriteria">search criteria (id, name, ..) to search</param>
    /// <returns>product entity objects based on the search criteria if exit,
    /// otherwise all products</returns>
    Task<List<Product>> SearchAsync(ProductSearchCriteria searchCriteria);
}

