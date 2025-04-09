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
/// <summary>
/// Pure database interactions for cashier entity.
/// </summary>
internal class CashierRepository : EfCoreRepository<CSMSDbContext, Cashier>, ICashierRepository
{
    public CashierRepository(IDbContextProvider<CSMSDbContext> dbContextProvider) : base(dbContextProvider) { }

    public async Task<Cashier> AddAsync(Cashier cashier)
    {
        return await InsertAsync(cashier);
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var dbContext = await GetDbContextAsync();
        var cashierToDelete = await dbContext.Cashiers.FindAsync(id);
        if (cashierToDelete is null)
        {
            return false;
        }
        await DeleteAsync(cashierToDelete);
        return true;
    }

    public async Task<List<Cashier>> GetAllAsync()
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.Cashiers.ToListAsync();
    }

    public async Task<Cashier?> GetByIdAsync(Guid id)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.Cashiers.FindAsync(id);
    }

    public async Task<List<Cashier>> SearchAsync(CashierSearchCriteria cashierSearchCriteria)
    {
        var dbContext = await GetDbContextAsync();
        var cashier = await dbContext.Cashiers
                        .WhereIf(cashierSearchCriteria.Id != null, c => c.Id == cashierSearchCriteria.Id)
                        .WhereIf(!string.IsNullOrEmpty(cashierSearchCriteria.Name),
                        c => c.Name!.Contains(cashierSearchCriteria.Name!))
                       .ToListAsync();
        return cashier;

    }

    public async Task<Cashier> UpdateAsync(Cashier cashier)
    {
        var dbContext = await GetDbContextAsync();
        var existingCashier = await dbContext.Cashiers.FindAsync(cashier.Id);
        if(existingCashier == null)
        {
            return cashier;
        }

        existingCashier.Name = cashier.Name;
        //var a = dbContext.Entry(existingCashier).State;

        await dbContext.SaveChangesAsync();
        return cashier;
    }
}

