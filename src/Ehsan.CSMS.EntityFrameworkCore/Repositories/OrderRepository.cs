using Ehsan.CSMS.Entities;
using Ehsan.CSMS.EntityFrameworkCore;
using Ehsan.CSMS.IRepositories;
using Ehsan.CSMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Ehsan.CSMS.Repositories
{
    internal class OrderRepository : EfCoreRepository<CSMSDbContext, Order>, IOrderRepository
    {
        private readonly ILogger<OrderRepository> _logger;
        //private readonly IDiagnosticContext _diagnosticContext;
        public OrderRepository(
            IDbContextProvider<CSMSDbContext> dbContextProvider, 
            ILogger<OrderRepository> logger) : base(dbContextProvider) 
        {
            _logger = logger;
        }

        public async Task<Order> AddAsync(Order order)
        {
            _logger.LogInformation("{ClassName}.{MethodName}", 
                nameof(OrderRepository), 
                nameof(AddAsync));
            // dublicate key 
            return await InsertAsync(order);
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var dbContext = await GetDbContextAsync();
            var ordersToDelete = await dbContext.Orders.FindAsync(id);
            if (ordersToDelete is null)
            {
                return false;
            }
            await DeleteAsync(ordersToDelete);
            return true;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            var dbContext = await GetDbContextAsync();
            return await dbContext.Orders.Include(property => property.Cashier)
                .Include(property => property.Customer)
                .Include(property => property.OrderDetails)
                .Include(property => property.Invoice)
                .OrderBy(o => o.OrderStatus)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            var dbContext = await GetDbContextAsync();
            // with include
            return await dbContext.Orders
                .Where(o => o.Id == id)
                .Include(o => o.Customer)
                .Include(o => o.Cashier)
                .Include(o => o.Invoice)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .ThenInclude(p => p.Category)
                .FirstAsync();
        }

        public async Task<List<Order>> SearchOrderAsync(OrderSearchCriteria orderSearchCriteria)
        {
            var dbContext = await base.GetDbContextAsync();
            var orders = await dbContext.Orders
                                        .Include(c => c.Customer)
                                        .Include(c => c.Cashier)
                                        .WhereIf(orderSearchCriteria.Id != null,
                                        c => c.Id == orderSearchCriteria.Id)
                                        .WhereIf(orderSearchCriteria.CashierId != null,
                                        c => c.CashierId == orderSearchCriteria.CashierId)
                                        .WhereIf(orderSearchCriteria.OrderStatus != 0,
                                        c => (int)c.OrderStatus == (int)orderSearchCriteria.OrderStatus)
                                        .OrderBy(o => o.OrderStatus)
                                        .ToListAsync();
            return orders;
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            var dbContext = await base.GetDbContextAsync();
            var orderToBeUpdate = await dbContext.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.Customer)
                .Include(o => o.Invoice)
                .FirstOrDefaultAsync(o => o.Id == order.Id);
            if (orderToBeUpdate == null)
            {
                return order;
            }
            // tracked when i modify the lp and be modified 
            //var c = dbContext.Entry(orderToBeUpdate.Customer).State;
            orderToBeUpdate.Customer = order.Customer;

            if(order.Cashier != null)
            {
                orderToBeUpdate.Cashier = order.Cashier;
            }

            orderToBeUpdate.OrderStatus = order.OrderStatus;
            orderToBeUpdate.TotalPrice = order.TotalPrice;

            foreach(var o in order.OrderDetails)
            {
                var orderDetail = orderToBeUpdate.OrderDetails
                    .FirstOrDefault(od => od.ProductID == o.ProductID);
                orderDetail.ProductID = o.ProductID;
                orderDetail.Quantity = o.Quantity;
                orderDetail.TotalPrice = o.TotalPrice;
            }

            orderToBeUpdate.Invoice.Discount = order.Invoice.Discount;
            orderToBeUpdate.Invoice.NetPrice = order.Invoice.NetPrice;
            var c = dbContext.Entry(orderToBeUpdate.Customer).State;

            // this is deleted not modi
            var a = dbContext.Entry(orderToBeUpdate).State;

            await dbContext.SaveChangesAsync();
            return order;
        }
    }
}
