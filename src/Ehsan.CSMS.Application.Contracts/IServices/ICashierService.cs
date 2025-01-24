using Ehsan.CSMS.Dtos;
using Ehsan.CSMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ehsan.CSMS.IService
{
    public interface ICashierService
    {// CRUD
         Task AddAsync(CashierDto cashier);
         Task DeleteByIdAsync(int id);
         Task UpdateAsync(CashierDto cashier);
         Task UpdateCashierNameAsync(int id, string name);
         Task<List<CashierDto>> GetAllAsync();
         Task<CashierDto> GetbyIdAsync(int id);

        Task<string> GetCashierNamebyId(int id);
        Task<List<CashierDto>> SearchAsync(CashierSearchCriteria cashierSearchCriteria);
    }
}
