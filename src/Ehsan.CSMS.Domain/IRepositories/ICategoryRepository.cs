using Ehsan.CSMS.Entities;
using Ehsan.CSMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Ehsan.CSMS.IRepositories
{
    public interface ICategoryRepository:IRepository<Category>
    {// CRUD
        Task AddAsync(Category category);
        Task AddManyAsync(IEnumerable<Category> category);
        Task DeleteAsync(Category category);
        Task DeleteByIdAsync(int id);
        Task UpdateAsync(Category category);
        Task UpdateCategoryNameAsync(int id, string name);
        Task<List<Category>> GetAllAsync();
        Task<Category> GetByNameAsync(string name);
        Task<Category> GetbyIdAsync(int id);
        Task<List<Category>> SearchAsync(CategorySearchCriteria categorySearchCriteria);

    }
}
