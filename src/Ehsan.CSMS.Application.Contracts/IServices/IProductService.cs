using Ehsan.CSMS.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ehsan.CSMS.Models;

namespace Ehsan.CSMS.IService
{// CRUD
    public interface IProductService
    {
        Task AddAsync(ProductDto product);
        Task DeleteAsync(ProductDto product);
        Task DeletebyIdAsync(int id);
        Task UpdateAsync(ProductDto product);
        Task UpdateProductNameAsync(int id, string name);
        Task UpdateProductPriceAsync(int id, double price);
        Task<List<ProductDto>> GetAllAsync();
        Task<ProductDto> GetbyIdAsync(int id);
        Task<double> GetPricePerUnintbyIdAsync(int id);
        Task<List<ProductDto>> SearchAsync(ProductSearchCriteria productSearchCriteria);
    }
}
