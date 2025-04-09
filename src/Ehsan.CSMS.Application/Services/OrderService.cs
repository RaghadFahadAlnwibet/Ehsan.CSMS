using Ehsan.CSMS.Dtos.CustomerDto;
using Ehsan.CSMS.Dtos.OrderDto;
using Ehsan.CSMS.Entities;
using Ehsan.CSMS.Enums;
using Ehsan.CSMS.Helper;
using Ehsan.CSMS.IRepositories;
using Ehsan.CSMS.IService;
using Ehsan.CSMS.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;

namespace Ehsan.CSMS.Services;

public class OrderService : ApplicationService, IOrderService
{
    private readonly IOrderRepository _OrderRepository;
    private readonly ICustomerService _CustomerService;
    private readonly IProductService _productService;
    private readonly ILogger<OrderService> _logger;

    public OrderService(
        IOrderRepository OrderRepository,
        ICustomerService CustomerService,
        IProductService productService,
        ILogger<OrderService> logger)
    {
        _OrderRepository = OrderRepository;
        _CustomerService = CustomerService;
        _productService = productService;
        _logger = logger;
    }
    public async Task<OrderResponse> AddAsync(OrderAddRequest? order)
    {
        // SRP
        _logger.LogInformation("Add Order of OrderService");

        if (order is null)
        { throw new ArgumentNullException(nameof(order)); }
        if (order.OrderDetailsRequests is null)
        { throw new ArgumentNullException(nameof(order.OrderDetailsRequests)); }
        if (order.Customer is null)
        { throw new ArgumentNullException(nameof(order.OrderDetailsRequests)); }

        ValidationHelper.ValidateModel(order);

        var orderEntity = ObjectMapper.Map<OrderAddRequest, Order>(order);

        double TotalPrice = await CalculateTotalPriceAsync(orderEntity);
        int existingLoyaltyPoints = await _CustomerService.GetLoyaltyPointByIdAsync(order.Customer.Id);
        int loyaltyPoints = CalculateLoyaltyPoints(TotalPrice, existingLoyaltyPoints); // 80 / 200 = 18
        double discount = CalculateDiscount(TotalPrice, loyaltyPoints);
        double NetPrice = CalculateNetPrice(TotalPrice, discount);

        var invoice = new Invoice();
        invoice.Discount = discount;
        invoice.NetPrice = NetPrice;

        orderEntity.Invoice = invoice;
        orderEntity.TotalPrice = TotalPrice;
        orderEntity.OrderStatus = OrderStatus.New;


        orderEntity.Customer.LoyaltyPoint = loyaltyPoints;
        // DB 
        if(orderEntity.Customer.Id != Guid.Empty)
        {
            order.Customer.LoyaltyPoint = loyaltyPoints;
            await _CustomerService.UpdateAsync(order.Customer);
            orderEntity.Customer = null;
        }
        var addedOrder = await _OrderRepository.AddAsync(orderEntity);

        return ObjectMapper.Map<Order, OrderResponse>(addedOrder);
    }
    private double CalculateNetPrice(double totalPrice, double discount)
    {
        return totalPrice - discount;
    }
    private static double CalculateDiscount(double totalPrice, int loyaltyPoints)
    {
        const int UnitsPerRiyal = 5;
        const double DiscountPerPoint = 0.25;

        int redeemablePoints = Math.Min(loyaltyPoints, UnitsPerRiyal * 20); 
        double discount = redeemablePoints / (double)UnitsPerRiyal;
        discount += loyaltyPoints * DiscountPerPoint;
        return discount;
    }
    private static int CalculateLoyaltyPoints(double totalPrice, int existingLoyaltyPoints) // 150, 120
    {
        const int MaxRedeemablePoints = 100;
        const double RiyalPerPoint = 10;

        int redeemablePoints = Math.Min(existingLoyaltyPoints, MaxRedeemablePoints); // 100

       // (each point = 1 Riyal discount)
        totalPrice -= redeemablePoints; // 150 - 100

        totalPrice = Math.Max(totalPrice, 0);

        int newPointsEarned = (int)(totalPrice / RiyalPerPoint); // 50 / 10

        // Remaining points after redemption + newly earned points
        return (existingLoyaltyPoints - redeemablePoints) + newPointsEarned; //(120 - 100) + 5 = 25
    }
    private async Task<double> CalculateTotalPriceAsync(Order order)
    {
        double TotalPrice = 0;


        foreach (var oi in order.OrderDetails!)
        {
            var productPrice = await _productService.GetPriceById(oi.ProductID);
            if (productPrice != null)
            {
                oi.ProductPrice = productPrice.Value;
                oi.TotalPrice = CalculateSubTotal(productPrice.Value, oi.Quantity);
                TotalPrice += oi.TotalPrice;
            }
        }

        return TotalPrice;
    }
    private double CalculateSubTotal(double price, int quantity)
    {
        return price * quantity;
    }

    public async Task<bool> DeleteByIdAsync(Guid? id)
    {
        if (id is null)
        { throw new ArgumentNullException(nameof(id)); }
        return await _OrderRepository.DeleteByIdAsync(id.Value);
    }

    public async Task<OrderResponse> UpdateAsync(OrderUpdateRequest? order)
    {
        if (order is null)
        { throw new ArgumentNullException(nameof(order)); }
        if (order.OrderDetailsUpdateRequest is null)
        { throw new ArgumentNullException(nameof(order.OrderDetailsUpdateRequest)); }
        ValidationHelper.ValidateModel(order);
        if(order.Id == Guid.Empty)
        { throw new ArgumentNullException(nameof(order.Id)); }

        var orderToUpdate = await _OrderRepository.GetByIdAsync(order.Id);
        if(orderToUpdate is null)
        {
            { throw new ArgumentException($"Invalid Order {0}", nameof(order.Id)); }
        }

        var orderToUpdateEntity = ObjectMapper.Map<OrderUpdateRequest, Order>(order);
        // not tracked 
        orderToUpdateEntity.Customer = orderToUpdate.Customer;
        double TotalPrice = await CalculateTotalPriceAsync(orderToUpdateEntity);
        int existingLoyaltyPoints = await _CustomerService.GetLoyaltyPointByIdAsync(orderToUpdateEntity.Customer?.Id);
        int loyaltyPoints = CalculateLoyaltyPoints(TotalPrice, existingLoyaltyPoints); // 80 / 200 = 18
        double discount = CalculateDiscount(TotalPrice, loyaltyPoints);
        double NetPrice = CalculateNetPrice(TotalPrice, discount);

        // not tracked
        orderToUpdateEntity.Invoice = new Invoice();

        orderToUpdateEntity.Invoice.Discount = discount;
        orderToUpdateEntity.Invoice.NetPrice = NetPrice;
        
        orderToUpdateEntity.TotalPrice = TotalPrice;

        if(orderToUpdateEntity.Customer != null)
        {
            orderToUpdateEntity.Customer.LoyaltyPoint = loyaltyPoints;
        }
        var updatedOrder = await _OrderRepository.UpdateAsync(orderToUpdateEntity);
        return ObjectMapper.Map<Order, OrderResponse>(updatedOrder);
    }

    public async Task<IEnumerable<OrderResponse>> GetAllAsync()
    {
        var orders = await _OrderRepository.GetAllAsync();
        return ObjectMapper.Map<List<Order>, List<OrderResponse>>(orders);
    }

    public async Task<OrderResponse?> GetByIdAsync(Guid? id)
    {
        if (id is null)
        { throw new ArgumentNullException(nameof(id)); }

        var order = await _OrderRepository.GetByIdAsync(id.Value);
        return ObjectMapper.Map<Order?, OrderResponse>(order);
    }

    public async Task<IEnumerable<OrderResponse>> SearchOrderAsync(OrderSearchCriteria searchCriteria)
    {
        var orders = await _OrderRepository.SearchOrderAsync(searchCriteria);
        return ObjectMapper.Map<List<Order>, List<OrderResponse>>(orders);

    }

}


