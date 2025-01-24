using Ehsan.CSMS.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ehsan.CSMS.Models;

namespace Ehsan.CSMS.IService
{
    public interface ICategoryServices
    {// CRUD
        Task AddAsync(CategoryDto category);
        Task DeleteAsync(CategoryDto category);
        Task DeleteByIdAsync(int id);
        Task UpdateAsync(CategoryDto category);
        Task UpdateCategoryNameAsync(int id, string name);
        Task<List<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByNameAsync(string name);
        Task<CategoryDto> GetbyIdAsync(int id);
        Task<List<CategoryDto>> SearchAsync(CategorySearchCriteria categorySearchCriteria);

    }
}
