using Ehsan.CSMS.Entities;
using Ehsan.CSMS.EntityFrameworkCore;
using Ehsan.CSMS.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Ehsan.CSMS.Repositories;

internal class OrderDetailsRepository : EfCoreRepository<CSMSDbContext, OrderDetail>, IOrderDetailsRepository
{
    public OrderDetailsRepository(IDbContextProvider<CSMSDbContext> dbContextProvider) : base(dbContextProvider) { }

    public async Task<List<OrderDetail>> GetByIdAsync(Guid id)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.OrderDetails
            .Include(orderDetail => orderDetail.Order)
            .ThenInclude(order => order.Customer)
            .Include(orderDetail => orderDetail.Order)
            .ThenInclude(order => order.Cashier)
            .Include(orderDetail => orderDetail.Product)
            .ThenInclude(product => product.Category)
            .ToListAsync();
    }
}
