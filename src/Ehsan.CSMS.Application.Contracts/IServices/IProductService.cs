using Ehsan.CSMS.Dtos.ProductDto;
using Ehsan.CSMS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ehsan.CSMS.IService;
/// <summary>
/// Product services contract
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Add a new product request object to the data store
    /// </summary>
    /// <param name="product">product request object to add</param>
    /// <returns>Returns product request object</returns>
    Task<ProductResponse> AddAsync(ProductAddRequest? product);
    /// <summary>
    /// Delete a product request object based on the given id from the data store
    /// </summary>
    /// <param name="id">product request object id to delete</param>
    /// <returns>Returns true if successful delete, otherwise false</returns>
    Task<bool> DeleteByIdAsync(Guid? id);
    /// <summary>
    /// Update a product request object in the data store
    /// </summary>
    /// <param name="product">product request object to update</param>
    /// <returns>Returns product request object after updating</returns>
    Task<ProductResponse> UpdateAsync(ProductUpdateRequest? product);
    /// <summary>
    /// Get all product request objects from the data store
    /// </summary>
    /// <returns>Returns all product request objects</returns>
    Task<IEnumerable<ProductResponse>> GetAllAsync();
    /// <summary>
    /// Get a product request object based on the given id from the data store
    /// </summary>
    /// <param name="id">product request object id to search</param>
    /// <returns>Returns product request object if exit, otherwise null</returns>
    Task<ProductResponse?> GetByIdAsync(Guid? id);
    /// <summary>
    /// Get a product entity price from the data store by Id
    /// </summary>
    /// <param name="id">product entity price to search</param>
    /// <returns>Returns product entity price, 
    /// otherwise null</returns>
    Task<double?> GetPriceById(Guid? Id);
    /// <summary>
    /// Get all product request objects from the data store based on the given search criteria
    /// </summary>
    /// <param name="searchCriteria">search criteria (id, name, ...) to search</param>
    /// <returns>Returns all product request objects based on the given search criteria if exit,
    /// otherwise all products </returns>
    Task<IEnumerable<ProductResponse>> SearchAsync(ProductSearchCriteria searchCriteria);
}
