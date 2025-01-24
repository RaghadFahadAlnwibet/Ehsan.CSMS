using Ehsan.CSMS.Entities;
using Ehsan.CSMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Ehsan.CSMS.IRepositories
{
    public interface ICashierRepository: IRepository<Cashier>
    {
        Task AddAsync(Cashier cashier);
        Task AddManyAsync(IEnumerable<Cashier> cashiers);
        Task DeleteAsync(Cashier cashier);
        Task DeleteByIdAsync(int id);
        Task UpdateAsync(Cashier cashier);
        Task UpdateCashierNameAsync(int id, string name);
        Task<List<Cashier>> GetAllAsync();
        Task<Cashier> GetByNameAsync(string name);

        Task<string> GetCashierNamebyId(int id);
        Task<Cashier> GetbyIdAsync(int id);
        Task<List<Cashier>> SearchAsync(CashierSearchCriteria cashierSearchCriteria);
    }
}
