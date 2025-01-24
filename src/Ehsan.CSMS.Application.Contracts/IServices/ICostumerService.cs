using Ehsan.CSMS.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ehsan.CSMS.Models;

namespace Ehsan.CSMS.IService
{// CRUD
    public interface ICostumerService
    {
        Task<CostumerDto> AddAsync(CostumerDto customer);
        Task DeleteAsync(CostumerDto customer);
        Task DeleteByIdAsync(int id);
        Task UpdateAsync(CostumerDto customer);
        Task UpdateCustomerNameAsync(int id, string name);
        Task<List<CostumerDto>> GetAllAsync();
        Task<CostumerDto> GetByNameAsync(string name);
        Task<CostumerDto> GetbyIdAsync(int id);
        Task<int> GetLoyalityPointbyId(int id);
        Task<CostumerDto?> GetCustomerByMobileNumberAsync(string mobileNumber);
        Task<List<CostumerDto>> SearchAsync(CostumerSearchCriteria costumerSearchCriteria);
        Task UpdateLoyalityPointAsync(int id, int loyaltyPoints);
    }
}
