using Ehsan.CSMS.Entities;
using Ehsan.CSMS.EntityFrameworkCore;
using Ehsan.CSMS.IRepositories;
using Ehsan.CSMS.Models;
using Microsoft.EntityFrameworkCore;
using Polly;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Ehsan.CSMS.Repositories
{
    internal class ProductRepository: EfCoreRepository<CSMSDbContext, Product>, IProductRepository
    {
        public ProductRepository(IDbContextProvider<CSMSDbContext> dbContextProvider) : base(dbContextProvider) // IDbContextProvider<TDbContext> dbContextProvider
        { }
        public async Task AddAsync(Product product)
        {
            try
            {
                await base.InsertAsync(product);
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }
        public async Task DeleteAsync(Product product)
        {
            try
            {
                 await base.DeleteAsync(product);
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }
        public async Task DeletebyIdAsync(int id)
        {
            var context = await base.GetDbContextAsync();
            try 
            {
                await context.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM Product Where Id = {id}");
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }
        public async Task UpdateAsync(Product product)
        {
            try
            {
                  await base.UpdateAsync(product);
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }
       public async Task UpdateProductNameAsync(int id, string name)
        {
            var context = await base.GetDbContextAsync();
            try
            {
                await context.Database.ExecuteSqlInterpolatedAsync($"UPDATE Product SET ProductName = {name} WHERE Id={id}");
            } 
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }

        public async Task UpdateProductPriceAsync(int id, double price)
        {
            var context = await base.GetDbContextAsync();
            try
            {
                await context.Database.ExecuteSqlInterpolatedAsync($"UPDATE Product SET ProductPrice = {price} WHERE Id={id}");
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var dbcontext = await base.GetDbContextAsync();
            try
            {
                var products = await dbcontext.Products
                                              .Include(c => c.Category)
                                              .ToListAsync();
                return products;// await base.GetListAsync(includeDetails: true);
            }
            catch (DbException exception)
            {
                throw new InvalidOperationException("An error occurred while retrieving products.", exception);
            }
        }
        public async Task<Product> GetbyIdAsync(int id)
        {
            var dbcontext = await base.GetDbContextAsync();
            try 
            {
               var products = await dbcontext.Products
                    .Include(c=>c.Category)                   
                    .Where(c => c.Id == id).FirstOrDefaultAsync();// id of the product of the send it id 
               return products;
            }
            catch (DbException exception)
            {
                throw new InvalidOperationException("An error occurred while retrieving products.", exception);
            }
        }

        public async Task<List<Product>> SearchAsync(ProductSearchCriteria productSearchCriteria)
        {
            var dbContext = await base.GetDbContextAsync();
            try
            {
                var products = await dbContext.Products
                    .Include(c=>c.Category)
                                               .WhereIf(productSearchCriteria.Id != null, c => c.Id == productSearchCriteria.Id)
                                               .WhereIf(!string.IsNullOrEmpty(productSearchCriteria.ProductName), c => c.ProductName.Contains(productSearchCriteria.ProductName))
                                               .WhereIf(productSearchCriteria.CategoryId!=null, c => c.CategoryId== productSearchCriteria.CategoryId)
                                               .ToListAsync();
                return products;
            }
            catch (DbException exception)
            {
                throw new InvalidOperationException("An error occurred while searching for the products.", exception);
            }
        }

        public async Task<double> GetPricePerUnintbyIdAsync(int id)
        {
            var dbContext = await base.GetDbContextAsync();
            try
            {
                var productPrice = await dbContext.Products
                                    .Where(c => c.Id == id) 
                                    .Select(c=>c.ProductPrice)
                                    .FirstOrDefaultAsync();

                return productPrice ;
            }
            catch (DbException exception)
            {
                return 0;
            }
        }
    }
}
