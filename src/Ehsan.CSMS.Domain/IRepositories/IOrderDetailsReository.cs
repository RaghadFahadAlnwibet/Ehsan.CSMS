using Ehsan.CSMS.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Ehsan.CSMS.IRepositories;
/// <summary>
/// Represents data access logic for order details entity.
/// </summary>
public interface IOrderDetailsRepository : IRepository<OrderDetail>
{
    /// <summary>
    /// Get an order details entity object based on the given order detail id
    /// </summary>
    /// <param name="id">order details entity object id to search</param>
    /// <returns>Returns order details entity object if exit, otherwise null</returns>
    Task<List<OrderDetail>> GetByIdAsync(Guid id);
}
