using Ehsan.CSMS.Dtos.CashierDto;
using Ehsan.CSMS.Entities;
using Ehsan.CSMS.Helper;
using Ehsan.CSMS.IRepositories;
using Ehsan.CSMS.IService;
using Ehsan.CSMS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Ehsan.CSMS.Services;

public class CashierService : ApplicationService, ICashierService
{
    // DP
    private readonly ICashierRepository _cashierRepository;

    public CashierService
        (ICashierRepository cashierRepository)
    {
        _cashierRepository = cashierRepository;
    }

    public async Task<CashierResponse> AddAsync(CashierAddRequest? cashier)
    {
        if (cashier is null)
        { throw new ArgumentNullException(nameof(cashier)); }
        // SRP 
        ValidationHelper.ValidateModel(cashier);

        // from the ApplicationService 
        var cashierEntity = ObjectMapper.Map<CashierAddRequest, Cashier>(cashier);
        await _cashierRepository.AddAsync(cashierEntity);
        return ObjectMapper.Map<Cashier, CashierResponse>(cashierEntity);
    }

    public Task<bool> DeleteByIdAsync(Guid? id)
    {
        if (id is null)
        { throw new ArgumentNullException(nameof(id)); }
        return _cashierRepository.DeleteByIdAsync(id.Value);
    }

    public async Task<IEnumerable<CashierResponse>> GetAllAsync()
    {
        var cahiers = await _cashierRepository.GetAllAsync();
        return ObjectMapper.Map<List<Cashier>, List<CashierResponse>>(cahiers);
    }

    public async Task<CashierResponse?> GetByIdAsync(Guid? id)
    {
        if (id is null)
        { throw new ArgumentNullException(nameof(id)); }
        var cashier = await _cashierRepository.GetByIdAsync(id.Value);
        return ObjectMapper.Map<Cashier?, CashierResponse>(cashier);
    }

    public async Task<IEnumerable<CashierResponse>> SearchAsync(CashierSearchCriteria searchCriteria)
    {
        var cashiers = await _cashierRepository.SearchAsync(searchCriteria);
        return ObjectMapper.Map<List<Cashier>, List<CashierResponse>>(cashiers);
    }

    public async Task<CashierResponse> UpdateAsync(CashierUpdateRequest? cashier)
    {
        if (cashier is null)
        { throw new ArgumentNullException(nameof(cashier)); }
        ValidationHelper.ValidateModel(cashier);
        if (cashier.Id == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(cashier.Id));
        }

        var cashierToUpdate = await _cashierRepository.GetByIdAsync(cashier.Id);
        if (cashierToUpdate is null)
        {
            { throw new ArgumentException($"Invalid Customer {0}", nameof(cashier.Id)); }
        }

        cashierToUpdate.Name = cashier.Name;
        var updatedCashier = await _cashierRepository.UpdateAsync(cashierToUpdate);
        return ObjectMapper.Map<Cashier, CashierResponse>(updatedCashier); ;
    }
}
