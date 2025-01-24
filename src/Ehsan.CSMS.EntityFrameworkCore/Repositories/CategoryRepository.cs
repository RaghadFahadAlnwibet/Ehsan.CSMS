using Ehsan.CSMS.Entities;
using Ehsan.CSMS.EntityFrameworkCore;
using Ehsan.CSMS.IRepositories;
using Ehsan.CSMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Ehsan.CSMS.Models;

namespace Ehsan.CSMS.Repositories
{
    internal class CategoryRepository : EfCoreRepository<CSMSDbContext, Category>, ICategoryRepository
    {
        public CategoryRepository(IDbContextProvider<CSMSDbContext> dbContextProvider) :base(dbContextProvider) // IDbContextProvider<TDbContext> dbContextProvider
        { }
        public async Task AddAsync(Category category)
        {
            try
            {
                await InsertAsync(category);
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }
        public async Task AddManyAsync(IEnumerable<Category> categories)
        {
            try
            {
               await base.InsertManyAsync(categories);
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }
        public async Task DeleteAsync(Category category)
        {
            try
            {
               await base.DeleteAsync(category);
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }
        public async Task DeleteByIdAsync(int id)
        {
            var dbcontext = await GetDbContextAsync();
            try
            {
                await dbcontext.Database.ExecuteSqlAsync($"DELETE FROM Category Where Id= {id}");
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }
        public async Task UpdateAsync(Category category)
        {
            try
            {
                await base.UpdateAsync(category);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
        public async Task UpdateCategoryNameAsync(int id, string name)
        {
            var context = await base.GetDbContextAsync();
            try
            {
                await context.Database.ExecuteSqlInterpolatedAsync($"UPDATE Category SET CategoryName = {name} WHERE Id={id}");
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }
        public async Task<List<Category>> GetAllAsync()
        {
            {
                try
                {
                    return await base.GetListAsync();
                }
                catch (DbException exception)
                {
                    throw new InvalidOperationException("An error occurred while retrieving category.", exception);
                }
            }
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            var dbcontext = await base.GetDbContextAsync();
            try
            {
                var categories = await dbcontext.Categories.Where(c => c.CategoryName == name).FirstOrDefaultAsync();
                return categories;
            }
            catch (DbException exception)
            {
                throw new InvalidOperationException("An error occurred while retrieving category.", exception);
            }
        }

        public async Task<Category> GetbyIdAsync(int id)
        {
            var dbcontext = await base.GetDbContextAsync();
            try
            {
                var categories = await dbcontext.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
                return categories;
            }
            catch (DbException exception)
            {
                throw new InvalidOperationException("An error occurred while retrieving category.", exception);
            }
        }

        public async Task<List<Category>> SearchAsync(CategorySearchCriteria categorySearchCriteria)
        {
            var dbContext = await base.GetDbContextAsync();
            try
            {
                var category = await dbContext.Categories
                                                .WhereIf(categorySearchCriteria.Id != null, c => c.Id == categorySearchCriteria.Id)
                                                .WhereIf(!string.IsNullOrEmpty(categorySearchCriteria.CategoryName), c => c.CategoryName.Contains(categorySearchCriteria.CategoryName))
                                                .ToListAsync();
                return category;
            }
            catch (DbException exception)
            {
                throw new InvalidOperationException("An error occurred while searching for the category.", exception);
            }
        }
    }
}
