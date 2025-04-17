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
            _logger.LogInformation("{ClassName}.{MethodName}",
                        nameof(OrderRepository),
                        nameof(UpdateAsync));

            var _dbContext = await base.GetDbContextAsync();

            var orderEntity = await _dbContext.Orders
                .Include(i => i.Invoice)
                .Include(od => od.OrderDetails)
                .FirstOrDefaultAsync(o => o.Id == order.Id);
            
            if (orderEntity == null)
            {
                throw new ArgumentException($"Order {order.Id} not found");
            }
            if (orderEntity.OrderDetails == null)
            {
                throw new ArgumentException($"Order details of Order {order.Id} not found");
            }
            if (orderEntity.Invoice == null)
            {
                throw new ArgumentException($"invoice of Order {order.Id} not found");
            }
            if (order.OrderDetails == null)
            {
                throw new ArgumentException($"Order details of Order {order.Id} is null");
            }
            if (order.Invoice == null)
            {
                throw new ArgumentException($"invoice details of Order {order.Id} is null");
            }

            orderEntity.OrderStatus = order.OrderStatus;
            orderEntity.TotalPrice = order.TotalPrice;
            orderEntity.Invoice.Discount = order.Invoice.Discount;
            orderEntity.Invoice.NetPrice = order.Invoice.NetPrice;


            var incomingIds = order.OrderDetails.Select(o => o.Id).ToHashSet();
            foreach (var existing in orderEntity.OrderDetails.ToList())
            {
                if (!incomingIds.Contains(existing.Id))
                    _dbContext.OrderDetails.Remove(existing);
            }
            var s = _dbContext.Entry(orderEntity).State;

            var orderDetailsTemp = new List<OrderDetail>();
            foreach (var orderDetails in order.OrderDetails)
            {
                var orderDetail = orderEntity.OrderDetails
                    .FirstOrDefault(od => od.Id == orderDetails.Id);
               
                if(orderDetail == null)
                {

                    orderDetailsTemp.Add(new OrderDetail
                    {
                        OrderId = orderDetails.OrderId,
                        ProductID = orderDetails.ProductID,
                        Quantity = orderDetails.Quantity,
                        ProductPrice = orderDetails.ProductPrice,
                        TotalPrice = orderDetails.TotalPrice,
                    });
                }
                else
                {
                    orderDetail.Quantity = orderDetails.Quantity;
                    orderDetail.TotalPrice = orderDetails.TotalPrice;
                }
            }
            orderDetailsTemp.ToList().ForEach(temp => orderEntity.OrderDetails.Add(temp));



            await _dbContext.SaveChangesAsync();
            return order;
        }
    }
}

