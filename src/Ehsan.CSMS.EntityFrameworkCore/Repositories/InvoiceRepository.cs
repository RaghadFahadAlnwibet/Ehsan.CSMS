using Ehsan.CSMS.Entities;
using Ehsan.CSMS.EntityFrameworkCore;
using Ehsan.CSMS.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Ehsan.CSMS.Repositories;

internal class InvoiceRepository : EfCoreRepository<CSMSDbContext, Invoice>, IInvoiceRepository
{
    public InvoiceRepository(IDbContextProvider<CSMSDbContext> dbContextProvider) : base(dbContextProvider) { }

    public async Task<Invoice?> GetByIdAsync(Guid id)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.Invoices
            .FindAsync(id);
    }
}
