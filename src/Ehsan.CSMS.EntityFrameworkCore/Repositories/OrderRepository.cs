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
    internal class OrderRepository: EfCoreRepository<CSMSDbContext, Order>, IOrderRepository
    {
        public OrderRepository(IDbContextProvider<CSMSDbContext> dbContextProvider): base(dbContextProvider) { }
        public async Task AddAsync(Order order)
        {
            var dbContext = await base.GetDbContextAsync();

            try
            {
                await InsertAsync(order);

            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message);
                throw new InvalidOperationException("Failed to insert order into the database", exception);
            }
        }

        public async Task AddManyAsync(IEnumerable<Order> order)
        {
            try
            {
                await base.InsertManyAsync(order);
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }
        public async Task DeleteAsync(Order order)
        {
            try
            {
                await base.DeleteAsync(order);
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
                await dbcontext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM [Order] WHERE Id= {id}");
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
        public async Task UpdateAsync(Order order)
        {
            try
            {

               await base.UpdateAsync(order);
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }
        public async Task<List<Order>> GetAllAsync()
        {
            var dbcontext = await base.GetDbContextAsync();
            try
            {
                var orders = await dbcontext.Orders
                                            .Include(c => c.Customer)
                                            .Include(c => c.Cashier)
                                            .Include(c=>c.OrderDetails)
                                            .ThenInclude(c => c.Product)
                                            .ToListAsync();
                return orders;
            }
            catch (DbException exception)
            {
                throw new InvalidOperationException("An error occurred while retrieving orders.", exception);
            }
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            var dbcontext = await base.GetDbContextAsync();
            try
            {
                var orders = await dbcontext.Orders
                                            .Include(c=>c.Customer)
                                            .Include(c=>c.Cashier)
                                            .Include(c => c.OrderDetails)
                                            .ThenInclude(c=>c.Product)
                                            .ThenInclude(c=>c.Category)
                                            .Where(c => c.Id == id)
                                            .SingleOrDefaultAsync();
                return orders;
            }
            catch (DbException exception)
            {
                throw new InvalidOperationException("An error occurred while retrieving orders.", exception);
            }
            
        }

        public async Task<List<Order>> SearchOrderAsync(OrderSearchCriteria orderSearchCriteria)
        {
            var dbContext = await base.GetDbContextAsync();
            try { 
            var orders = await dbContext.Orders
                                        .Include(c => c.Customer)
                                        .Include(c => c.Cashier)
                                        .WhereIf(orderSearchCriteria.OrderId != null, c => c.Id == orderSearchCriteria.OrderId)
                                        .WhereIf(orderSearchCriteria.CashierId != null, c => c.CashierId == orderSearchCriteria.CashierId)
                                        .ToListAsync();
              

                return orders;
            }
            catch (DbException exception)
            {
                throw new InvalidOperationException("An error occurred while searching for the Orders.", exception);
            }
        }
        public async Task<List<Order>> SerachByOrderStatusAsync(int status)
        {
            var dbContext = await base.GetDbContextAsync();
            try
            {
                var orders = await dbContext.Orders
                                       .Include(c => c.Customer)
                                       .Include(c => c.Cashier)
                                       .Where(c => (int)c.OrderStatus == status)
                                       .ToListAsync();
                if (status==0)
                {
                    orders = await dbContext.Orders
                                            .Include(c => c.Customer)
                                            .Include(c => c.Cashier)
                                            .ToListAsync();
                }
                return orders;
            }
            catch (DbException exception)
            {
                throw new InvalidOperationException("An error occurred while searching for the Orders.", exception);
            }
        }
        public async Task<double> GetTotalPriceByIdAsync(int id) // 2 
        {
            var dbContext = await base.GetDbContextAsync();
            try
            {
                double TotalPrice =  dbContext.Orders
                                              .Where(c=> c.Id == id)
                                              .Select(c=> c.TotalPrice)
                                              .FirstOrDefault();
                return TotalPrice;
            }
            catch (DbException exception)
            {
                throw new InvalidOperationException("An error occurred while searching for the Orders.", exception);
            }
        }
    }
}
