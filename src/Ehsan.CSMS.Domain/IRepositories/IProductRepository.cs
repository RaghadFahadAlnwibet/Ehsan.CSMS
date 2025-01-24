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
    public interface IProductRepository : IRepository<Product>
    { // CRUD
        Task AddAsync(Product product);
        Task DeleteAsync(Product product);
        Task DeletebyIdAsync(int id);
        Task UpdateAsync(Product product);
        Task UpdateProductNameAsync(int id, string name);
        Task UpdateProductPriceAsync(int id, double price);
        Task<List<Product>> GetAllAsync();
        Task<Product> GetbyIdAsync(int id);
        Task<double> GetPricePerUnintbyIdAsync(int id);
        Task<List<Product>> SearchAsync(ProductSearchCriteria productSearchCriteria);
    }
}

