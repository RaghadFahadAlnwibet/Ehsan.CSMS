using Ehsan.CSMS.Dtos.CategoryDto;
using Ehsan.CSMS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ehsan.CSMS.IService;
/// <summary>
/// Category services contract
/// </summary>
public interface ICategoryServices
{
    /// <summary>
    /// Add a new category request object to the data store
    /// </summary>
    /// <param name="category">category request object to add</param>
    /// <returns>Returns category request object</returns>
    Task<CategoryResponse> AddAsync(CategoryAddRequest? category);
    /// <summary>
    /// Delete a category request object based on the given id from the data store
    /// </summary>
    /// <param name="id">category request object to delete</param>
    /// <returns>Returns true is successful delete, otherwise false</returns>
    Task<bool> DeleteByIdAsync(Guid? id);
    /// <summary>
    /// Update a category request object in the data store
    /// </summary>
    /// <param name="category">category request object to update</param>
    /// <returns>Returns category request object after updating</returns>
    Task<CategoryResponse> UpdateAsync(CategoryUpdateRequest? category);
    /// <summary>
    /// Get all category request objects from the data store
    /// </summary>
    /// <returns>Returns all category request objects</returns>
    Task<IEnumerable<CategoryResponse>> GetAllAsync();
    /// <summary>
    /// Get a category request object based on the given id from the data store
    /// </summary>
    /// <param name="id">category request object id to search</param>
    /// <returns>Returns category request object based on the given id from the data store if exit, otherwise null</returns>
    Task<CategoryResponse?> GetByIdAsync(Guid? id);
    /// <summary>
    /// Get all category request objects from the data store based on the given search criteria
    /// </summary>
    /// <param name="searchCriteria">search criteria (id, name, ..) to search</param>
    /// <returns>Returns category request objects based on the given search criteria if exit, 
    /// otherwise all categories</returns>
    Task<IEnumerable<CategoryResponse>> SearchAsync(CategorySearchCriteria searchCriteria);
}
