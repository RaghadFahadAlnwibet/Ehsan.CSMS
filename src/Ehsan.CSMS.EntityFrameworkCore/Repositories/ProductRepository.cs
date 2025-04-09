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

internal class ProductRepository : EfCoreRepository<CSMSDbContext, Product>, IProductRepository
{
    public ProductRepository(IDbContextProvider<CSMSDbContext> dbContextProvider) : base(dbContextProvider) // IDbContextProvider<TDbContext> dbContextProvider
    { }

    public async Task<Product> AddAsync(Product product)
    {
        return await InsertAsync(product);
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var dbContext = await GetDbContextAsync();
        var productToDelete = await dbContext.Products.FindAsync(id);
        if (productToDelete is null)
        {
            return false;
        }
        await DeleteAsync(productToDelete);
        return true;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.Products
            .Include(product => product.Category)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.Products
            .FindAsync(id);
    }

    public async Task<Product?> GetBynameAsync(string name)
    {
        var dbContext = await base.GetDbContextAsync();
        return dbContext.Products.Where(product => product.Name == name)
            .FirstOrDefault();

    }

    public async Task<double?> GetPriceById(Guid Id)
    {
        var dbContext = await base.GetDbContextAsync();
        return await dbContext.Products.Where(Product => Product.Id == Id)
            .Select(product => product.Price)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Product>> SearchAsync(ProductSearchCriteria productSearchCriteria)
    {
        var dbContext = await base.GetDbContextAsync();
        var products = await dbContext.Products
                        .Include(c => c.Category)
                        .WhereIf(productSearchCriteria.Id != null, c => c.Id == productSearchCriteria.Id)
                        .WhereIf(!string.IsNullOrEmpty(productSearchCriteria.Name),
                        c => c.Name.Contains(productSearchCriteria.Name!))
                        .WhereIf(productSearchCriteria.CategoryId != null,
                        c => c.CategoryId == productSearchCriteria.CategoryId)
                        .ToListAsync();
        return products;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        var dbContext = await base.GetDbContextAsync();
        var existingProduct = await dbContext.Products.FindAsync(product.Id);
        if(existingProduct is null)
        {
            return product;
        }
        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.CategoryId = product.CategoryId;

        await dbContext.SaveChangesAsync();
        return product;

    }
}
