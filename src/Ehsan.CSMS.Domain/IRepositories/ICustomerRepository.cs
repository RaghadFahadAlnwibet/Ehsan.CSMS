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
    public interface ICustomerRepository : IRepository<Customer>
    {// CRUD
        Task<Customer> AddAsync(Customer customer);

        Task AddManyAsync(IEnumerable<Customer> customer);
        Task DeleteAsync(Customer customer);
        Task DeleteByIdAsync(int id);
        Task UpdateAsync(Customer customer);
        Task UpdateCustomerNameAsync(int id, string name);
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetByNameAsync(string name);
        Task<Customer> GetbyIdAsync(int id);
        Task<int> GetLoyalityPointbyId(int id);
        Task<Customer> GetCustomerByMobileNumberAsync(string mobileNumber);
        Task<List<Customer>> SearchAsync(CostumerSearchCriteria costumerSearchCriteria);
        Task UpdateLoyalityPointAsync(int id, int loyaltyPoints);


    }
}
