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
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;

namespace Ehsan.CSMS.Services
{
    public class OrderService : ApplicationService, IOrderService
    {
        private readonly IOrderRepository _OrderRepository;

        private readonly IOrderDeatilsService _OrderDetailsService;

        private readonly ICostumerService _CustomerService;

        private readonly ICustomerRepository _CostumerRepository;

        public OrderService(IOrderRepository OrderRepository, ICustomerRepository CostumerRepository, IOrderDeatilsService _OrderDetailsService)
        {
            _OrderRepository = OrderRepository;
            _CostumerRepository = CostumerRepository;
        }

        public async Task AddAsync(OrderDto order)
        {
            try
            {
                var orderEntity = ObjectMapper.Map<OrderDto, Order>(order);

                int loyalityPoint = await _CostumerRepository.GetLoyalityPointbyId(orderEntity.CustomerId);

                if (loyalityPoint > 0)
                {
                    if (loyalityPoint >= 100)
                    {
                        orderEntity.discount = 100 / 5;
                        loyalityPoint -= 100;
                    }
                    else
                    {
                        int DiscountPercentage = (int)(loyalityPoint / 5);
                        orderEntity.discount = (double)((orderEntity.TotalPrice * DiscountPercentage) / 100);
                        loyalityPoint = 0;
                    }

                    orderEntity.NetPrice = orderEntity.TotalPrice - orderEntity.discount;
                }
                else
                {
                    orderEntity.discount = 0;
                    orderEntity.NetPrice = (double)orderEntity.TotalPrice;

                    loyalityPoint += (int)(orderEntity.NetPrice / 10);
                }

                // Save to DB 
                await _CostumerRepository.UpdateLoyalityPointAsync(orderEntity.CustomerId, loyalityPoint);
                
                var newCustomerEntity = await _CostumerRepository.AddAsync(orderEntity.Customer);

                if (newCustomerEntity != null)
                {
                    orderEntity.CustomerId = newCustomerEntity.Id;
                }
              
                // should not return any thing
                 await _OrderRepository.AddAsync(orderEntity);

            }
            catch (DbException exception)
            {
                throw new InvalidOperationException("Failed to add order", exception);
            }
        }



        public async Task DeleteAsync(OrderDto order)
        {
            try
            {
                var orderEntity = ObjectMapper.Map<OrderDto, Order>(order);
                await _OrderRepository.DeleteAsync(orderEntity);
            } 
            catch (DbException exception)
            {
                new InvalidOperationException("Failed to delete order", exception);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            try 
            {
                await _OrderRepository.DeleteByIdAsync(id);
            }
            catch (DbException exception)
            {
                new InvalidOperationException("Failed to delete order", exception);
            }
        }

        public async Task<List<OrderDto>> GetAllAsync()
        {
            try 
            {
                var orders = await _OrderRepository.GetAllAsync();
                var orderDtos = ObjectMapper.Map<List<Order>, List<OrderDto>>(orders);
                return orderDtos;
            } catch (DbException exception) 
            {
                return null;
            }  
        }

        public async Task<OrderDto> GetByIdAsync(int id)
        {
            try {
                var orders = await _OrderRepository.GetByIdAsync(id);
                var orderDtos = ObjectMapper.Map<Order, OrderDto>(orders);
                return orderDtos;
            }
            catch(DbException exception)
            { 
                return null;
            }
        }

        public async Task<double> GetTotalPriceByIdAsync(int id)
        {
            try
            {
                var netPrice = await _OrderRepository.GetTotalPriceByIdAsync(id);
                return netPrice;
            }
            catch (DbException exception)
            {
                return 0;
            }
        }

        public async Task<List<OrderDto>> SearchOrderAsync(OrderSearchCriteria OrderSearchcriteria)
        {
            try
            {
                var orders = await _OrderRepository.SearchOrderAsync(OrderSearchcriteria);
                var orderDto = ObjectMapper.Map<List<Order>, List<OrderDto>>(orders);
                return orderDto;
            }
            catch (DbException exception)
            {
                return null;
            }
        }

        public async Task<List<OrderDto>> SerachByOrderStatusAsync(int status)
        {
            var ordersEntity = await _OrderRepository.SerachByOrderStatusAsync(status);
            var OrderDto = ObjectMapper.Map<List<Order>, List<OrderDto>>(ordersEntity);
            return OrderDto;
        }

        public async Task UpdateAsync(OrderDto order)
        {
            try 
            {

              
                var orderEntity = ObjectMapper.Map<OrderDto, Order>(order);
                await _OrderRepository.UpdateAsync(orderEntity);
            }
            catch (DbException exception) 
            {
                new InvalidOperationException("Failed to update order", exception);
            }  
        }
    }
}
