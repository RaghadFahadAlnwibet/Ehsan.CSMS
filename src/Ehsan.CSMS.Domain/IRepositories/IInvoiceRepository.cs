using Ehsan.CSMS.Entities;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Ehsan.CSMS.IRepositories;
/// <summary>
/// Represents data access logic for invoice entity
/// </summary>
public interface IInvoiceRepository : IRepository<Invoice>
{
    /// <summary>
    /// Get an invoice entity object based on the given id
    /// </summary>
    /// <param name="id">invoice entity object id to search</param>
    /// <returns>Returns invoice entity object if exit, otherwise null</returns>
    Task<Invoice?> GetByIdAsync(Guid id);
}
