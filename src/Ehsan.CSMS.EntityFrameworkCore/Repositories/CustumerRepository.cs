using Ehsan.CSMS.Entities;
using Ehsan.CSMS.EntityFrameworkCore;
using Ehsan.CSMS.IRepositories;
using Ehsan.CSMS.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EventBus.Local;
using Volo.Abp.Linq;

namespace Ehsan.CSMS.Repositories
{
    internal class CustomerRepository : EfCoreRepository<CSMSDbContext, Customer>, ICustomerRepository
    {


        public CustomerRepository(IDbContextProvider<CSMSDbContext> dbContextProvider) : base(dbContextProvider) // IDbContextProvider<TDbContext> dbContextProvider
        { }

        public async Task<Customer> AddAsync(Customer customer)
        {
            var dbContext = await base.GetDbContextAsync();

            try
            {
                if (await GetCustomerByMobileNumberAsync(customer.ContactInfo) == null)
                {
                    var NewCustomer =  await base.InsertAsync(customer, autoSave: true);
                    return NewCustomer;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the customer: {ex.Message}");
                throw new InvalidOperationException("An error occurred while adding the customer.", ex);
            }

        }

        public async Task AddManyAsync(IEnumerable<Customer> customers)
        {
            try
            {
                await base.InsertManyAsync(customers);
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }
        public async Task DeleteAsync(Customer customer)
        {
            try
            {
                await base.DeleteAsync(customer);
            }
            catch (DbException exceptio)
            {
                Console.WriteLine(exceptio.Message.ToString());
            }
        }
        public async Task DeleteByIdAsync(int id)
        {
            var dbcontext = await GetDbContextAsync();
            try
            {
                await dbcontext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM Customer Where Id = {id}");
            }
            catch (DbException exceptio)
            {
                Console.WriteLine(exceptio.Message.ToString());
            }
        }
        public async Task UpdateAsync(Customer customer) 
        {
            
            if(await GetCustomerByMobileNumberAsync(customer.ContactInfo) == null ||  GetCustomerByMobileNumberAsync(customer.ContactInfo).Id == customer.Id) // new mobile number is updated 
            {

                await base.UpdateAsync(customer);
            }
            else
            {
                Console.WriteLine("mobile number is already taken");
            }
        }


        public async Task UpdateCustomerNameAsync(int id, string name)
        {
            var context = await base.GetDbContextAsync();
            try
            {
                await context.Database.ExecuteSqlInterpolatedAsync($"UPDATE Customer SET CustomerName = {name} WHERE Id={id}");
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }
        public async Task<List<Customer>> GetAllAsync()
        {
            var dbcontext = await base.GetDbContextAsync();

            try
            {
                var customers = dbcontext.Costumers
                    //.Include(c => c.Orders) 
                    .ToList();
                return customers;
            }
            catch (DbException exception)
            {
                throw new InvalidOperationException("An error occurred while retrieving costumers.", exception);
            }
        }
        public async Task<Customer> GetByNameAsync(string name)
        {
            var dbcontext = await base.GetDbContextAsync();
            try
            {
                var customers = await dbcontext.Costumers.Where(c => c.CustomerName == name).FirstOrDefaultAsync();
                return customers;
            }
            catch (DbException exception)
            {
                throw new InvalidOperationException("An error occurred while retrieving costumers.", exception);
            }
        }
        public async Task<Customer> GetbyIdAsync(int id)
        {
            var dbcontext = await base.GetDbContextAsync();
            try
            {
                var customers = await dbcontext.Costumers.Where(c => c.Id == id).FirstOrDefaultAsync();
                return customers;
            }
            catch (DbException exception)
            {
                throw new InvalidOperationException("An error occurred while retrieving costumers.", exception);
            }

        }

        public async Task<List<Customer>> SearchAsync(CostumerSearchCriteria costumerSearchCriteria)
        {
            var dbContext = await base.GetDbContextAsync();
            try
            {
                var customers = await dbContext.Costumers
                                                .WhereIf(costumerSearchCriteria.Id != null, c => c.Id == costumerSearchCriteria.Id)
                                                .WhereIf(!string.IsNullOrEmpty(costumerSearchCriteria.CustomerName), c => c.CustomerName.Contains(costumerSearchCriteria.CustomerName))
                                                .WhereIf(costumerSearchCriteria.mobileNumber != null, c => c.ContactInfo.Contains(costumerSearchCriteria.mobileNumber)) // mobile number must be unique
                                                .ToListAsync();

                return customers;
            }
            catch (DbException exception)
            {
                throw new InvalidOperationException("An error occurred while searching for the costumers.", exception);
            }
        }

        public async Task<int> GetLoyalityPointbyId(int id)
        {
            var dbContext = await base.GetDbContextAsync();
            try
            {
                int? loyaltyPoints = await dbContext.Costumers
                                                    .Where(c => c.Id == id)
                                                    .Select(c => (int?)c.LoyaltyPoints)
                                                    .FirstOrDefaultAsync();

                return loyaltyPoints ?? 0;
            }
            catch (DbException ex)
            {
                throw new InvalidOperationException("Error fetching Loyalty Points.", ex);
            }
        }


        public async Task UpdateLoyalityPointAsync(int id, int loyaltyPoints)
        {
            var dbContext = await base.GetDbContextAsync();
            try
            {
                await dbContext.Database.ExecuteSqlInterpolatedAsync($"UPDATE Customer SET LoyaltyPoints = {loyaltyPoints} WHERE Id={id}");
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }

        public async Task<Customer> GetCustomerByMobileNumberAsync(string mobileNumber) 
        {
            var dbContext = await base.GetDbContextAsync();
            try
            {
                var customer = await dbContext.Costumers
                                                    .Where(c => c.ContactInfo.Contains(mobileNumber)) // mobile number is unique
                                                    .FirstOrDefaultAsync();

                return customer; // if it null means it is new customer 
            }
            catch (DbException ex)
            {
                throw new InvalidOperationException("Error Retriving Loyalty Points.", ex);
            }
        }


       

    }
}



