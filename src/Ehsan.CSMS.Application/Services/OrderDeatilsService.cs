using AutoMapper;
using Ehsan.CSMS.Dtos;
using Ehsan.CSMS.Entities;
using Ehsan.CSMS.IRepositories;
using Ehsan.CSMS.IService;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;

namespace Ehsan.CSMS.Services
{
    public class OrderDeatilsService : ApplicationService, IOrderDeatilsService
    {
        private readonly IOrderDeatilsRepository _OederDeatilsRepository;
        public OrderDeatilsService(IOrderDeatilsRepository iOederDeatilsRepository)
        {
            _OederDeatilsRepository = iOederDeatilsRepository;
        }

        public async Task AddAsync(OrderDeatilsDto orderDetail)
        {
            try
            {
                var orderDeatilsEntity = ObjectMapper.Map<OrderDeatilsDto, OrderDetail>(orderDetail);
                await _OederDeatilsRepository.AddAsync(orderDeatilsEntity);
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }

        public async Task AddManyAsync(IEnumerable<OrderDeatilsDto> orderDetails) // collectotion from orderDto 
        {
            try
            {
                var orderDetailsEntity = ObjectMapper.Map<List<OrderDeatilsDto>, List<OrderDetail>>((List<OrderDeatilsDto>)orderDetails);
               await _OederDeatilsRepository.AddManyAsync(orderDetailsEntity);
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
                throw new InvalidOperationException("Failed to add order details", exception);
            }
        }


        public async Task DeleteAsync(OrderDeatilsDto orderDetail)
        {
            try
            {
                var orderDeatilsEntity = ObjectMapper.Map<OrderDeatilsDto, OrderDetail>(orderDetail);
                await _OederDeatilsRepository.DeleteAsync(orderDeatilsEntity);
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }

        public async Task DeletebyIdAsync(int id)
        {
            try
            {
                await _OederDeatilsRepository.DeletebyIdAsync(id);
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }

        public async Task<List<OrderDeatilsDto>> GetAllAsync()
        {
            try 
            {
                var OrderDeatils = await _OederDeatilsRepository.GetAllAsync();
                var OrderDeatilsDto = ObjectMapper.Map<List<OrderDetail>, List<OrderDeatilsDto>>(OrderDeatils);
                return OrderDeatilsDto;
            } catch(DbException exception) 
            {
                return null;
            }   
        }

        public async Task<OrderDeatilsDto> GetbyIdAsync(int id)
        {
            try
            {
                var OrderDeatils = await _OederDeatilsRepository.GetbyIdAsync(id);
                var OrderDeatilsDto = ObjectMapper.Map<OrderDetail, OrderDeatilsDto>(OrderDeatils);
                return OrderDeatilsDto;
            }
            catch (DbException exception)
            {
                return null;
            }
        }

        public async Task<int> GetNoOfItemsByOrderId(int id)
        {
            return await _OederDeatilsRepository.GetNoOfItemsByOrderId(id);
        }

        public async Task<decimal> GetTotalPricebyIdAsync(int id)
        {

            try
            {
                decimal TotalPrice =  await _OederDeatilsRepository.GetTotalPricebyIdAsync(id);
                return TotalPrice;
            }
            catch (DbException exception)
            {
                throw new InvalidOperationException("An error occurred while retrieving TotalPrice", exception);
            }
        }

        public async Task UpdateAync(OrderDeatilsDto orderDetail)
        {
            try
            {
                var orderDeatilsEntity = ObjectMapper.Map<OrderDeatilsDto, OrderDetail>(orderDetail);
                await _OederDeatilsRepository.UpdateAsync(orderDeatilsEntity);
            }
            catch (DbException exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
        }
    }
}
