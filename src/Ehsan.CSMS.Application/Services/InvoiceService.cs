using Ehsan.CSMS.Dtos.InvoiceDto;
using Ehsan.CSMS.Entities;
using Ehsan.CSMS.IRepositories;
using Ehsan.CSMS.IServices;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Ehsan.CSMS.Services;

public class InvoiceService : ApplicationService, IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;
    public InvoiceService(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<InvoiceResponse?> GetByIdAsync(Guid? id)
    {
        if (id is null)
        { throw new ArgumentNullException(nameof(id)); }
        var invoice = await _invoiceRepository.GetByIdAsync(id.Value);
        return ObjectMapper.Map<Invoice?, InvoiceResponse>(invoice);
    }
}
