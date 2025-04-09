using Ehsan.CSMS.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Ehsan.CSMS.Models;
using Ehsan.CSMS.Web.Models.CustomerViewModel;
using Ehsan.CSMS.Dtos.CustomerDto;
using Ehsan.CSMS.Web.Filters.ActionFilter;
namespace Ehsan.CSMS.Web.Controllers;

public class CustomerController : Controller
{
    private readonly ICustomerService _costumerService;
    public CustomerController(ICustomerService costumerService)
    {
        _costumerService = costumerService;
    }
    // GET: CustomerController
    public async Task<ActionResult> Index(CostumerViewModel costumerViewModel)
    {
        if (costumerViewModel.costumerSearchCriteria == null)
        {
            costumerViewModel.costumerSearchCriteria = new CustumerSearchCriteria();
        }

        costumerViewModel.costumers = await _costumerService.SearchAsync(costumerViewModel.costumerSearchCriteria);
        return View(costumerViewModel);
    }

    // GET: CustomerController/Details/5
    public async Task<ActionResult> Details(Guid? id)
    {
        var customer = await _costumerService.GetByIdAsync(id);
        if(customer is null)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(customer);
    }
    //// GET: CustomerController/Create
    public ActionResult Create()
    {
        return View();
    }
    // POST: CustomerController/Create
    [HttpPost]
    [TypeFilter(typeof(ModelValidatorActionFilter))]
    public async Task<ActionResult> Create(CustomerAddRequest? costumerRequest)
    {
        await _costumerService.AddAsync(costumerRequest);
        return RedirectToAction(nameof(Index));
    }
    // GET: CustomerController/Edit/5
    public async Task<ActionResult> Edit(Guid? id)
    {
        var customer = await _costumerService.GetByIdAsync(id);
        if(customer == null)
        {
            return RedirectToAction(nameof(Index));
        }
        var customerUpdate = new CustomerUpdateRequest()
        {
            Id = customer.Id,
            Name = customer.Name,
            MobileNumber = customer.MobileNumber,
            LoyaltyPoint = customer.LoyaltyPoint
        };
        return View(customerUpdate);
    }
    // POST: CustomerController/Edit/5
    [HttpPost]
    [TypeFilter(typeof(ModelValidatorActionFilter))]
    public async Task<ActionResult> Edit(CustomerUpdateRequest? costumerRequest)
    {
        await _costumerService.UpdateAsync(costumerRequest);
        return RedirectToAction(nameof(Index));
    }
    public async Task<ActionResult> Delete(Guid? id)
    {
        var isDeleted = await _costumerService.DeleteByIdAsync(id);
        if (isDeleted)
        {
            return RedirectToAction(nameof(Index));
        }
        return BadRequest("Failed to delete Customer");

    }

    public IActionResult GetCustomerInformationBy(string mobileNumber)
    {
        return ViewComponent("CustomerOrder", mobileNumber);
    }
}
