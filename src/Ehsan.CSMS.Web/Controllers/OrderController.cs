using Castle.Core.Resource;
using Ehsan.CSMS.Dtos;
using Ehsan.CSMS.Entities;
using Ehsan.CSMS.IService;
using Ehsan.CSMS.Models;
using Ehsan.CSMS.Services;
using Ehsan.CSMS.Web.Components;
using Ehsan.CSMS.Web.Models;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ehsan.CSMS.Web.Controllers
{
    public class OrderController : Controller
    {
        public IOrderService _OrderService;
        public ICostumerService _CostumerService;
        public ICashierService _CashierService;

        public IOrderDeatilsService _OrderDeatilsService;

        public IProductService _ProductService;


        public OrderController(IOrderService orderService, ICostumerService CostumerService, ICashierService CashierService, IProductService productService, IOrderDeatilsService orderDeatilsService)
        {
            _OrderService = orderService;
            _CostumerService = CostumerService;
            _CashierService = CashierService;
            _ProductService = productService;
            _OrderDeatilsService = orderDeatilsService;
        }
        // GET: Order
        public async Task<ActionResult> Index()
        {
            var orderList = await _OrderService.GetAllAsync();
            OrderViewModel order = new OrderViewModel
            {
                orders = orderList
            };
            // inested of model you can have store procedure 
            await LoadLookups();
            return View(order);
        }

        public async Task<OrderDto> GetOrderbyId(int orderId)
        {
            var order = await _OrderService.GetByIdAsync(orderId);
            return order;
        }
        // GET: Order/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var order = await _OrderService.GetByIdAsync(id);
           

            return View(order);
        }

        private async Task LoadLookups()
        {
            var cashiers = await _CashierService.GetAllAsync();
            var products = await _ProductService.GetAllAsync();
            ViewBag.Cashiers = new SelectList(cashiers, "Id", "CashierName");
            ViewBag.Products = new SelectList(products, "Id", "ProductName");


        }
        // GET: Order/Create
        public async Task<ActionResult> Create()
        {
           
            await LoadLookups();
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateOrder(CreateOrderViewModel orderView)
        {
            if (orderView.order.CreationTime == DateTime.MinValue)
            {
                orderView.order.CreationTime = DateTime.Now;
            }

            var CartLength = orderView.selectedPrpducts.Count();
            for (int i = 0; i < CartLength; i++)
            {
                orderView.order.OrderDetails.Add(new OrderDeatilsDto()
                {
                    ProductId = orderView.selectedPrpducts[i],
                    Quantity = orderView.qunatity[i],
                    PricePerUnit = orderView.pricePerUnit[i],
                    TotalPrice = orderView.subTotals[i]
                });
            }

            await _OrderService.AddAsync(orderView.order);

            return RedirectToAction(nameof(Index));
        }

       public async Task<ActionResult> ShowInvoice(int orderId)
        {
            var order = await _OrderService.GetByIdAsync(orderId);
            return View("invoice", order);
        }

        public async Task<PartialViewResult> Customerinfo(string ContactInfo)
        {
            var customer = await _CostumerService.GetCustomerByMobileNumberAsync(ContactInfo);
            if (customer == null)
            {
                customer = new CostumerDto()
                {
                    CustomerName = string.Empty,
                    ContactInfo = string.Empty
                };
            }
            OrderViewModel orderView = new OrderViewModel()
            {
                order = new OrderDto(){
                  Customer = customer,
                }
            };
            return PartialView("_createCustomer", orderView); // draw 
        }

        public async Task<int> GetNoOfItems(int orderId)
        {
            var noOfItems = await _OrderDeatilsService.GetNoOfItemsByOrderId(orderId);
            return noOfItems;
        }

        public async Task<PartialViewResult> ProductInfo()
        {
            var products = await _ProductService.GetAllAsync();
            CreateOrderViewModel createOrderViewModel = new CreateOrderViewModel() { 
                products = products
            };
            return PartialView("_createProduct", createOrderViewModel); // draw 
        }
        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _OrderService.GetByIdAsync(id);
            await LoadLookups();
            return View(order);
        }
        // POST: Order/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(OrderDto order)
        {
            try
            {
                if (order.LastModificationTime == DateTime.MinValue)
                {
                    order.LastModificationTime = DateTime.Now; 
                }

                if (order.CreationTime == DateTime.MinValue)
                {
                    order.CreationTime = DateTime.Now;
                }

                await _OrderService.UpdateAsync(order);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(order);
            }
        }

        // GET: Order/Delete/5
        public async Task<ActionResult> DeleteOrder(int id) // on delete cascade 
        {
            try
            {
                await _OrderService.DeleteByIdAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> filtterByOrderStatus(int id)
        {
            var order = await _OrderService.SerachByOrderStatusAsync(id);
            OrderViewModel orderView = new OrderViewModel()
            {
                orders = order
            };
            await LoadLookups();
            TempData["selected"] = id;
            return View("Index", orderView);
        }
        public async Task<ActionResult> SearchFromModelView(OrderViewModel order){
            if(order.orderSearchCriteria == null)
                order.orderSearchCriteria = new CSMS.Models.OrderSearchCriteria();
            try
            {
                var Filteredorders = await _OrderService.SearchOrderAsync(order.orderSearchCriteria);
                order.orders = Filteredorders;
                await LoadLookups();

                return View("Index", order); 
            }
            catch
            {
                return View();
            }
        }
    }
}
