using AutoMapper;
using Ehsan.CSMS.Dtos;
using Ehsan.CSMS.Entities;
using Ehsan.CSMS.IRepositories;
using Ehsan.CSMS.IService;
using Ehsan.CSMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;

namespace Ehsan.CSMS.Services
{
    public class CostumerService : ApplicationService, ICostumerService
    {
        private readonly ICustomerRepository _CostumerRepository;
        public CostumerService(ICustomerRepository iCostumerRepository)
        {
            _CostumerRepository = iCostumerRepository;
        }

        public async Task<CostumerDto> AddAsync(CostumerDto customer)
        {
            try 
            {
                var costumerEntity = ObjectMapper.Map<CostumerDto, Customer>(customer);
                var customerEntity= await _CostumerRepository.AddAsync(costumerEntity);
                var costumerDto = ObjectMapper.Map<Customer, CostumerDto>(customerEntity);
                return costumerDto;
            }
            catch (DbException exception)
            {
                return null;
            } 
        }


        public async Task DeleteAsync(CostumerDto customer)
        {
            try 
            {
                var costumerEntity = ObjectMapper.Map<CostumerDto, Customer>(customer);
                await _CostumerRepository.DeleteAsync(costumerEntity);
            } 
            catch (DbException exception) 
            {
                new InvalidOperationException("Failed to add customer", exception);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            try 
            {
                await _CostumerRepository.DeleteByIdAsync(id);
            }
            catch (DbException exception) 
            {
                new InvalidOperationException("Failed to delete customer", exception);
            }
        }
        public async Task<List<CostumerDto>> GetAllAsync()
        {
            try 
            {
                var costumers = await _CostumerRepository.GetAllAsync();
                var costumerDtos = ObjectMapper.Map<List<Customer>, List<CostumerDto>>(costumers);
                return costumerDtos;
            }
            catch (DbException exception) 
            {
                throw new InvalidOperationException("An error occurred while retrieving costumers.", exception);
            }
        }

        public async Task<CostumerDto> GetbyIdAsync(int id)
        {
            try 
            {
                var costumers = await _CostumerRepository.GetbyIdAsync(id);
                var costumerDtos = ObjectMapper.Map<Customer, CostumerDto>(costumers);
                return costumerDtos;
            }
            catch(DbException exception) 
            {
                return null;
            } 
        }

        public async Task<CostumerDto> GetByNameAsync(string name)
        {
            try
            {
                var costumers = await _CostumerRepository.GetByNameAsync(name);
                var costumerDtos = ObjectMapper.Map<Customer, CostumerDto>(costumers);
                return costumerDtos;
            } 
            catch(DbException exception)
            {
                return null;
            }  
        }

        public async Task<CostumerDto?> GetCustomerByMobileNumberAsync(string mobileNumber)
        {
            try
            {
                var customer = await _CostumerRepository.GetCustomerByMobileNumberAsync(mobileNumber);
                var costumerDtos = ObjectMapper.Map<Customer, CostumerDto>(customer);
                return costumerDtos;
            }
            catch (DbException exception)
            {
                return null;
            }

        }
        public async Task<int> GetLoyalityPointbyId(int id)
        {
            try
            {
                var loyalitypoint = await _CostumerRepository.GetLoyalityPointbyId(id);
                return loyalitypoint;
            }
            catch (DbException exception)
            {
                throw new InvalidOperationException("An error occurred while retrieving LoyaltyPoints.", exception);
            }
        }

        public async Task<List<CostumerDto>> SearchAsync(CostumerSearchCriteria costumerSearchCriteria)
        {
            try 
            {
                var costumers = await _CostumerRepository.SearchAsync(costumerSearchCriteria);
                var customerDtos = ObjectMapper.Map<List<Customer>, List<CostumerDto>>(costumers);
                return customerDtos;
            }
            catch(DbException exception)
            {
                return null;
            }
        }

        public async Task UpdateAsync(CostumerDto customer)
        {
            try 
            {
                var costumerEntity = ObjectMapper.Map<CostumerDto, Customer>(customer);
                await _CostumerRepository.UpdateAsync(costumerEntity);
            }
            catch(DbException exception)
            {
                new InvalidOperationException("Failed to update customer", exception);
            } 
        }
        public async Task UpdateCustomerNameAsync(int id, string name)
        {
            try 
            {
                await _CostumerRepository.UpdateCustomerNameAsync(id, name);
            }
            catch (DbException exception)
            {
                new InvalidOperationException("Failed to update customer", exception);
            }
        }

        public async Task UpdateLoyalityPointAsync(int id, int loyaltyPoints)
        { 
            try
            {
                await _CostumerRepository.UpdateLoyalityPointAsync(id, loyaltyPoints);
    }
            catch (DbException exception)
            {
                new InvalidOperationException("Failed to update customer", exception);
}
        }
    }
}
