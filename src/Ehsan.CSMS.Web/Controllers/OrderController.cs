using Ehsan.CSMS.Dtos.OrderDetailsDto;
using Ehsan.CSMS.Dtos.OrderDto;
using Ehsan.CSMS.Entities;
using Ehsan.CSMS.IService;
using Ehsan.CSMS.Models;
using Ehsan.CSMS.Web.Filters.ActionFilter;
using Ehsan.CSMS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ehsan.CSMS.Web.Controllers;

public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    public OrderController(
        IOrderService orderService)
    {
        _orderService = orderService;
    }
    // GET: Order
    [TypeFilter(typeof(CatchierListActionFilter))]
    public async Task<IActionResult> Index(OrderViewModel orderViewModel)
    {
        if(orderViewModel.OrderSearchCriteria == null)
        {
            orderViewModel.OrderSearchCriteria = new OrderSearchCriteria();
        }
        orderViewModel.orders = 
            await _orderService.SearchOrderAsync(orderViewModel.OrderSearchCriteria);
        
        return View(orderViewModel);
    }

    [HttpGet]
    [TypeFilter(typeof(CatchierListActionFilter))]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [TypeFilter(typeof(CatchierListActionFilter))]
    public async Task<IActionResult> Create([FromBody] OrderAddRequest? orderRequest)
    {
        var redirectUrl = Url.Action("Index", "Order");
        if (orderRequest == null)
        {
            return Json(new { redirectTo = redirectUrl });
        }
        if (!ModelState.IsValid)
        {
            return Json(new { error = "Invalid data", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }
        await _orderService.AddAsync(orderRequest);

        return Json(new { redirectTo = redirectUrl });
    }

    public async Task<IActionResult> Details(Guid? id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if(order == null)
        {
            return RedirectToAction("Index");   
        }
        return View(order);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid? id)
    {
        var order =  await _orderService.GetByIdAsync(id);
        if(order == null || order.OrderDetailsResponses == null)
        {
            return RedirectToAction(nameof(Index));
        }
        var orderUpdateRequest = new OrderUpdateRequest();
        var orderDetailsUpdateRequest = new List<OrderDetailsUpdateRequest>();

        orderUpdateRequest.Id = order.Id;
        orderUpdateRequest.orderStatus = order.OrderStatus;
        foreach (var od in order.OrderDetailsResponses)
        {
            orderDetailsUpdateRequest.Add(new OrderDetailsUpdateRequest()
            {
                Id = od.Id,
                ProductID = od.ProductId,
                Quantity = od.Quantity,
                Product = od.Product,
                TotalPrice = od.TotalPrice,
            });
        }
        orderUpdateRequest.OrderDetailsUpdateRequest = orderDetailsUpdateRequest;
        return View(orderUpdateRequest);
    }
    [HttpPost]
    public async Task<IActionResult> Edit([FromBody] OrderUpdateRequest? orderRequest)
    {
        var redirectUrl = Url.Action("Index", "Order");
        if (orderRequest == null)
        {
            return Json(new { redirectTo = redirectUrl });
        }
        if (!ModelState.IsValid)
        {
            return Json(new { error = "Invalid data", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

        }
        await _orderService.UpdateAsync(orderRequest);

        return Json(new { redirectTo = redirectUrl });
    }

    public async Task<OrderResponse?> GetOrderById(Guid? id)
    {
        var order = await _orderService.GetByIdAsync(id);
        return order;
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var isDeleted = await _orderService.DeleteByIdAsync(id);
        if (isDeleted)
        {
            return RedirectToAction(nameof(Index));
        }
        
        return BadRequest("Failed to delete the order.");
    }
    public async Task<IActionResult> ShowInvoice(Guid? id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order == null)
        {
            return RedirectToAction(nameof(Index));
        }
        return new ViewAsPdf("Invoice", order, ViewData)
        {
            PageMargins = new Rotativa.AspNetCore.Options.Margins()
            {
                Top = 20,
                Right = 20,
                Bottom = 20,
                Left = 20
            },
            PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
        };

    }


}
