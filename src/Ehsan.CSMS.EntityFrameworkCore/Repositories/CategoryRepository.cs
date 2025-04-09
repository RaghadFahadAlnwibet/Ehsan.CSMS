using Ehsan.CSMS.Entities;
using Ehsan.CSMS.EntityFrameworkCore;
using Ehsan.CSMS.IRepositories;
using Ehsan.CSMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Ehsan.CSMS.Repositories;

internal class CategoryRepository : EfCoreRepository<CSMSDbContext, Category>, ICategoryRepository
{
    public CategoryRepository(IDbContextProvider<CSMSDbContext> dbContextProvider) : base(dbContextProvider) // IDbContextProvider<TDbContext> dbContextProvider
    { }

    public async Task<Category> AddAsync(Category category)
    {
        return await InsertAsync(category);
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var dbContext = await GetDbContextAsync();
        var category = await dbContext.Categories.FindAsync(id);
        if (category is null)
        {
            return false;
        }
        await DeleteAsync(category);
        return true;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.Categories
                    .Include(property => property.Products)
                    .ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.Categories
            .FindAsync(id);
    }

    public async Task<Category?> GetByNameAsync(string name)
    {
        var dbContext = await base.GetDbContextAsync();
        return dbContext.Categories.Where(c => c.Name == name)
            .FirstOrDefault();
    }

    public async Task<List<Category>> SearchAsync(CategorySearchCriteria categorySearchCriteria)
    {
        var dbContext = await base.GetDbContextAsync();
        var category = await dbContext.Categories
                                        .WhereIf(categorySearchCriteria.Id != null, c => c.Id == categorySearchCriteria.Id)
                                        .WhereIf(!string.IsNullOrEmpty(categorySearchCriteria.Name),
                                        c => c.Name.Contains(categorySearchCriteria.Name!)) // sql use like
                                        .ToListAsync();
        return category;

    }

    public async Task<Category> UpdateAsync(Category category)
    {
        var dbContext = await base.GetDbContextAsync();
        var exitingCategory = await dbContext.Categories.FindAsync(category.Id);
        if(exitingCategory == null)
        {
            return category;
        }
        exitingCategory.Name = category.Name;

        await dbContext.SaveChangesAsync();
        return category;
    }


}
