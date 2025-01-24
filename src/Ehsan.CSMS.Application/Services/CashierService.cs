using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Ehsan.CSMS.IService;
using Ehsan.CSMS.IRepositories;
using Ehsan.CSMS.Dtos;
using System.Data.Common;
using Ehsan.CSMS.Entities;
using Volo.Abp.ObjectMapping;
using static Ehsan.CSMS.Constant.Fields;
using AutoMapper;
using Ehsan.CSMS.Models;

namespace Ehsan.CSMS.Services
{
    public class CashierService : ApplicationService, ICashierService
    {
        private readonly ICashierRepository _CashierRepository;

        public CashierService(ICashierRepository icashierRepository)
        {
            _CashierRepository = icashierRepository;
        }

        public async Task AddAsync(CashierDto cashier)
        {
            try 
            {
                var cashierEntity = ObjectMapper.Map<CashierDto, Cashier>(cashier);
                await _CashierRepository.AddAsync(cashierEntity);
            } 
            catch (DbException exception) 
            {
                new InvalidOperationException("Failed to add chachier", exception);
            }
            }


        public async Task DeleteByIdAsync(int id)
        {
            try 
            {
                await _CashierRepository.DeleteByIdAsync(id);
            }
            catch (DbException exception) 
            {
                new InvalidOperationException("Failed to delete chachier", exception);
            }
        }

        public async Task UpdateAsync(CashierDto cashier)
        {
            try 
            {
                var cashierEntity = ObjectMapper.Map<CashierDto, Cashier>(cashier);
                await _CashierRepository.UpdateAsync(cashierEntity);
            }catch (DbException exception) 
            {
                new InvalidOperationException("Failed to update chachier", exception);
            }
        }

        public async Task UpdateCashierNameAsync(int id, string name)
        {
            try 
            {
                await _CashierRepository.UpdateCashierNameAsync(id, name);
            }
            catch (DbException exception) 
            {
                new InvalidOperationException("Failed to update chachier", exception);
            }
        }

        public async Task<List<CashierDto>> GetAllAsync()
        {
            try 
            {
                var cashiers = await _CashierRepository.GetListAsync();
                var cashierDtos = ObjectMapper.Map<List<Cashier>, List<CashierDto>>(cashiers);
                return cashierDtos;
            }
            catch (DbException exception)
            {
                return null;
            }
        }

        public async Task<CashierDto> GetbyIdAsync(int id)
        {
            try
            {
                var cashier = await _CashierRepository.GetbyIdAsync(id);
                var cashierDto = ObjectMapper.Map<Cashier, CashierDto>(cashier);
                return cashierDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<CashierDto>> SearchAsync(CashierSearchCriteria cashierSearchCriteria)
        {
            try 
            {
                var cashiers = await _CashierRepository.SearchAsync(cashierSearchCriteria);
                var cashierDtos = ObjectMapper.Map<List<Cashier> , List<CashierDto>>(cashiers);
                return cashierDtos;
            }
            catch (DbException)
            {
                return null;
            }
        }

        public async Task<string> GetCashierNamebyId(int id)
        {
            try
            {
                return await _CashierRepository.GetCashierNamebyId(id);
            }
            catch (DbException exception)
            {
                return null;
            }
        }
    }

}
