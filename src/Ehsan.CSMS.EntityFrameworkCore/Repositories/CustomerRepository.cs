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

internal class CustomerRepository : EfCoreRepository<CSMSDbContext, Customer>, ICustomerRepository
{
    public CustomerRepository(IDbContextProvider<CSMSDbContext> dbContextProvider) : base(dbContextProvider) // IDbContextProvider<TDbContext> dbContextProvider
    { }

    public async Task<Customer> AddAsync(Customer customer)
    {
        return await InsertAsync(customer);
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        // method injection 
        var dbContext = await GetDbContextAsync();
        var customerToDelete = await dbContext.Customers.FindAsync(id);
        if (customerToDelete is null)
        {
            return false;
        }
        await DeleteAsync(customerToDelete);
        return true;
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.Customers
            .ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.Customers
            .FindAsync(id);
    }

    public async Task<Customer?> GetByMobileNumberAsync(string mobileNumber)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.Customers
            .FirstOrDefaultAsync(customer =>
            customer.MobileNumber!.Equals(mobileNumber));
    }

    public async Task<int> GetLoyaltyPointByIdAsync(Guid id)
    {
        var dbContext = await GetDbContextAsync();
        var loyaltyPont = await dbContext.Customers
                            .Where(c => c.Id == id)
                            .Select(c => c.LoyaltyPoint)
                            .FirstOrDefaultAsync();   
            
        return loyaltyPont ?? 0;
    }
    public async Task<List<Customer>> SearchAsync(CustumerSearchCriteria costumerSearchCriteria)
    {
        var dbContext = await base.GetDbContextAsync();
        var customers = await dbContext.Customers
                                        .WhereIf(costumerSearchCriteria.Id != null, c => c.Id == costumerSearchCriteria.Id)
                                        .WhereIf(!string.IsNullOrEmpty(costumerSearchCriteria.Name),
                                        c => c.Name.Contains(costumerSearchCriteria.Name!))
                                        .WhereIf(costumerSearchCriteria.MobileNumber != null,
                                        c => c.MobileNumber.Equals(costumerSearchCriteria.MobileNumber!)) // mobile number must be unique
                                        .ToListAsync();
        return customers;
    }

    public async Task<Customer> UpdateAsync(Customer customer)
    {
        var dbContext = await GetDbContextAsync();
        var exitingCustomer = await dbContext.Customers.FindAsync(customer.Id);
        if(exitingCustomer == null)
        {
            return customer;
        }
        customer.Name = exitingCustomer.Name;
        customer.MobileNumber = exitingCustomer.MobileNumber;
        customer.LoyaltyPoint = exitingCustomer.LoyaltyPoint;

        await dbContext.SaveChangesAsync();
        return customer;
    }
}



