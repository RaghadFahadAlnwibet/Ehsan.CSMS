using Ehsan.CSMS.Entities;
using Ehsan.CSMS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Ehsan.CSMS.IRepositories;
/// <summary>
/// Represents data access logic for category entity   
/// </summary>
public interface ICategoryRepository : IRepository<Category>
{
    /// <summary>
    /// Add a new category entity object to the data store
    /// </summary>
    /// <param name="category">category entity object to add</param>
    /// <returns>Returns category entity object</returns>
    Task<Category> AddAsync(Category category);
    /// <summary>
    /// Delete a category entity object from the data store by id
    /// </summary>
    /// <param name="id">category entity object id to delete</param>
    /// <returns>Returns true if success delete, otherwise false</returns>
    Task<bool> DeleteByIdAsync(Guid id);
    /// <summary>
    /// Update a category entity object in the data store
    /// </summary>
    /// <param name="category">category entity object to update</param>
    /// <returns>category entity object after updating</returns>
    Task<Category> UpdateAsync(Category category);
    /// <summary>
    /// Get all category entity objects from the data store
    /// </summary>
    /// <returns>Returns all category entity objects</returns>
    Task<List<Category>> GetAllAsync();
    /// <summary>
    /// Get a category entity object from the data store by id
    /// </summary>
    /// <param name="id">category entity object to search</param>
    /// <returns>Returns category entity object if exit, otherwise null</returns>
    Task<Category?> GetByIdAsync(Guid id);
    /// <summary>
    /// Get a category entity object based on the given name from the data store
    /// </summary>
    /// <param name="id">category entity object name to search</param>
    /// <returns>Returns category entity object based on the given name from the data store if exit, 
    /// otherwise null</returns>
    Task<Category?> GetByNameAsync(string name);

    /// <summary>
    /// Search category entity objects from the data store by search criteria
    /// </summary>
    /// <param name="searchCriteria">search criteria (id, name, ..) to search</param>
    /// <returns>Returns category entity objects based on the search criteria if exit, 
    /// otherwise all categories</returns>
    Task<List<Category>> SearchAsync(CategorySearchCriteria searchCriteria);
}
