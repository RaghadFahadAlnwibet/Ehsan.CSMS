using Ehsan.CSMS.Dtos.InvoiceDto;
using System;
using System.Threading.Tasks;

namespace Ehsan.CSMS.IServices;
/// <summary>
/// Invoice service contract
/// </summary>
public interface IInvoiceService
{
    /// <summary>
    /// Get an invoice request object based on the given id
    /// </summary>
    /// <param name="id">invoice request object id to search</param>
    /// <returns>Returns invoice request object if exit, otherwise null</returns>
    Task<InvoiceResponse?> GetByIdAsync(Guid? id);
}
