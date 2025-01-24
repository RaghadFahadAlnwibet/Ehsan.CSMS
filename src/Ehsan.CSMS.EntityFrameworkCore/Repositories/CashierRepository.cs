using Ehsan.CSMS.Entities;
using Ehsan.CSMS.EntityFrameworkCore;
using System;
using Ehsan.CSMS.IRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Ehsan.CSMS.Models;

namespace Ehsan.CSMS.Repositories
{
    internal class CashierRepository: EfCoreRepository<CSMSDbContext, Cashier>, ICashierRepository
    {
        public CashierRepository(IDbContextProvider<CSMSDbContext> dbContextProvider): base(dbContextProvider) { }

        public async Task AddAsync(Cashier cashier)
        {
            try
            {
               await base.InsertAsync(cashier);
            }
            catch (DbException exception) 
            { 
                Console.WriteLine(exception.Message.ToString());  
            }
        }
        public async Task AddManyAsync(IEnumerable<Cashier> cashiers)
        {
            try
            {
                await base.InsertManyAsync(cashiers);
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }
        public async Task DeleteAsync(Cashier cashier)
        {
            try
            {
               await base.DeleteAsync(cashier);
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }
        public async Task DeleteByIdAsync(int id)
        {
            var dbcontext = await base.GetDbContextAsync();
            try
            {
                await dbcontext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM Cashier WHERE Id= {id}");
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        public async Task UpdateAsync(Cashier cashier)
        {
            try
            {
                await base.UpdateAsync(cashier);
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }
        public async Task UpdateCashierNameAsync(int id, string name)
        {
            var dbcontext = await base.GetDbContextAsync();
            try
            {
                await dbcontext.Database.ExecuteSqlInterpolatedAsync($"Update Cashier SET CashierName = {name} WHERE Id = {id}");
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }
        public async Task<List<Cashier>> GetAllAsync()
        {
            try
            {
                return await base.GetListAsync();
            }
            catch (DbException exception)
            {
                throw new InvalidOperationException("An error occurred while retrieving cashiers.", exception);
            }
        }
        public async Task<Cashier> GetByNameAsync(string name)
        {
            var dbcontext = await base.GetDbContextAsync();
            try
            {
                var cashiers = await dbcontext.Cashiers.Where(c => c.CashierName == name).FirstOrDefaultAsync<Cashier>();
                return cashiers;  
            }
            catch (DbException exception)
            {
                throw new InvalidOperationException("An error occurred while retrieving cashiers.", exception);
            }
        }
        public async Task<Cashier> GetbyIdAsync(int id)
        {
            var dbcontext = await base.GetDbContextAsync();
            try
            {
                var cashier = await dbcontext.Cashiers.Where(c=> c.Id == id).FirstOrDefaultAsync<Cashier>();
                return cashier;
            }
            catch(DbException exception)
            {
                throw new InvalidOperationException("An error occurred while retrieving cashiers.", exception);
            }
        }

        public async Task<List<Cashier>> SearchAsync(CashierSearchCriteria cashierSearchCriteria)
        {
                var dbContext = await base.GetDbContextAsync();
                try
                {                    var cashier = await dbContext.Cashiers
                                                 .WhereIf(cashierSearchCriteria.Id!=null ,c => c.Id==cashierSearchCriteria.Id)
                                                 .WhereIf(!string.IsNullOrEmpty( cashierSearchCriteria.CashierName), c => c.CashierName.Contains( cashierSearchCriteria.CashierName))
                                                 .ToListAsync();//  await dbContext.Cashiers.ToListAsync(); 

                return cashier;
                }
                catch (DbException exception)
                {
                    throw new InvalidOperationException("An error occurred while searching for the cashier.", exception);
                }
            

        }

        public async Task<string> GetCashierNamebyId(int id)
        {
            var dbcontext = await base.GetDbContextAsync();
            try
            {
                var cashierName = await dbcontext.Cashiers.Where(c => c.Id == id).Select(c=>c.CashierName).FirstOrDefaultAsync();
                return cashierName;
            }
            catch (DbException exception)
            {
                throw new InvalidOperationException("An error occurred while retrieving cashiers.", exception);
            }
        }
    }
}
