using Ehsan.CSMS.Entities;
using Ehsan.CSMS.EntityFrameworkCore;
using Ehsan.CSMS.IRepositories;
using Microsoft.EntityFrameworkCore;
using Polly;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Ehsan.CSMS.Repositories
{
    internal class OrderDeatilsRepository : EfCoreRepository<CSMSDbContext, OrderDetail>, IOrderDeatilsRepository
    {
        public OrderDeatilsRepository(IDbContextProvider<CSMSDbContext> dbContextProvider): base(dbContextProvider) { }

        public async Task AddAsync(OrderDetail orderDetail)
        {
            var dbcontext = await GetDbContextAsync();
            try
            {
                await base.InsertAsync(orderDetail);
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }

        public async Task AddManyAsync(IEnumerable<OrderDetail> orderDetails) 
        {
            var dbcontext = await GetDbContextAsync();

            try
            {
                var sqlCommand = new StringBuilder("INSERT INTO OrderDetail (ProductId, OrderId, TotalPrice, PricePerUnit, Quantity) VALUES ");

                foreach (var detail in orderDetails)
                {
                    sqlCommand.Append($"({detail.ProductId}, {detail.OrderId}, {detail.TotalPrice}, {detail.PricePerUnit}, {detail.Quantity}),");
                }

                sqlCommand.Length--;

                await dbcontext.Database.ExecuteSqlRawAsync(sqlCommand.ToString());
            }
            catch (DbException exception) {

                Console.WriteLine(exception.Message);
                throw new InvalidOperationException("Failed to add order details to the database", exception);

            }

        }
        public async Task DeleteAsync(OrderDetail orderDetail)
        {
            var dbcontext = await GetDbContextAsync();
            try
            {
                await base.DeleteAsync(orderDetail);
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }

        public async Task DeletebyIdAsync(int id)
        {
            var dbcontext = await GetDbContextAsync();
            try
            {
                await dbcontext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM OrderDetail WHERE Id == {id}");
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }

        public async Task<List<OrderDetail>> GetAllAsync()
        {
            var dbcontext = await GetDbContextAsync();
            try
            {
                var orderDetails = await dbcontext.OrderDetails
                                                  .Include(c => c.Product)
                                                  .Include(c => c.Order)
                                                  .ToListAsync();
                return orderDetails;
            }
            catch (DbException exception)
            {
                throw new InvalidOperationException("An error occurred while retrieving costumers.", exception);
            }
        }
        public async Task<OrderDetail> GetbyIdAsync(int id)
        {
            var dbcontext = await GetDbContextAsync();
            try
            {
                var orderDetails = await dbcontext.OrderDetails
                                                  .Include(c=>c.Product)
                                                  .Include(c => c.Order)
                                                  .Where(c=>c.Id== id)
                                                  .FirstOrDefaultAsync();
                return orderDetails;
            }
            catch (DbException exception)
            {
                throw new InvalidOperationException("An error occurred while retrieving costumers.", exception);
            }
        }

        public async Task<decimal> GetTotalPricebyIdAsync(int id)
        {
            var dbcontext = await GetDbContextAsync();
            try
            { // change order table to retrive total price from orderdetails
                decimal TotalPrice = await dbcontext.OrderDetails
                                                  .Where(c => c.Id == id)
                                                  .Select(c=> c.TotalPrice)
                                                  .FirstOrDefaultAsync();
                return TotalPrice;
            }
            catch (DbException exception)
            {
                throw new InvalidOperationException("An error occurred while retrieving costumers.", exception);
            }
        }

        public async Task UpdateAync(OrderDetail orderDetail)
        {
            var dbcontext = await GetDbContextAsync();
            try
            {
                await base.UpdateAsync(orderDetail);
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }

        public async Task<int> GetNoOfItemsByOrderId(int id)
        {
            var dbContext = await base.GetDbContextAsync();
            try
            {
                int NoOFitems =  dbContext.OrderDetails
                                         .Where(c => c.OrderId == id)
                                         .Sum(c => c.Quantity);
                return NoOFitems;
            }catch(DbException exception)
            {
                return 0;
            }
        }
    }
}
