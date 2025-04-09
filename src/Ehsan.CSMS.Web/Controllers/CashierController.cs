using Ehsan.CSMS.Dtos.CashierDto;
using Ehsan.CSMS.Entities;
using Ehsan.CSMS.IService;
using Ehsan.CSMS.Models;
using Ehsan.CSMS.Web.Filters.ActionFilter;
using Ehsan.CSMS.Web.Models.CashierViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Ehsan.CSMS.Web.Controllers;

public class CashierController : Controller
{
    private readonly ICashierService _cashierService;
    public CashierController(ICashierService CashierDtoService)
    {
        _cashierService = CashierDtoService;
    }
    // GET: CashierController
    //[TypeFilter(typeof(SearchSelectionActionFilter))]
    public async Task<IActionResult> Index(CashierViewModel cashierViewModel)
    {
        if (cashierViewModel.SearchCriteria is null)
        {
            cashierViewModel.SearchCriteria = new CashierSearchCriteria();
        }
        cashierViewModel.Cashiers = await _cashierService.SearchAsync(cashierViewModel.SearchCriteria);
        return View(cashierViewModel);
    }

    // GET: CashierController/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        var cashier = await _cashierService.GetByIdAsync(id);
        if (cashier is null)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(cashier);
    }

    //GET: CashierController/Create
    public IActionResult Create()
    {
        return View();
    }
    // POST: CashierController/Create
    [HttpPost]
    [TypeFilter(typeof(ModelValidatorActionFilter))]
    public async Task<IActionResult> Create(CashierAddRequest? cashierRequest)
    {
        await _cashierService.AddAsync(cashierRequest);
        return RedirectToAction(nameof(Index)); 
    }

    //// GET: CashierController/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        var cashier = await _cashierService.GetByIdAsync(id);
        if (cashier == null)
        {
            return RedirectToAction(nameof(Index));
        }
        var cashierUpdateRequest = new CashierUpdateRequest()
        {
            Id = cashier.Id,
            Name = cashier.Name
        };
        return View(cashierUpdateRequest);
    }

    //// POST: CashierController/Edit/
    [HttpPost]
    [TypeFilter(typeof(ModelValidatorActionFilter))]
    public async Task<ActionResult> Edit(CashierUpdateRequest? cashierRequest)
    {
        await _cashierService.UpdateAsync(cashierRequest);
        return RedirectToAction(nameof(Index));
    }

    public async Task<ActionResult> Delete(Guid? id)
    {
       var isDeleted = await _cashierService.DeleteByIdAsync(id);
        if (isDeleted)
        {
            return RedirectToAction(nameof(Index));
        }
        return BadRequest("Failed to delete the cashier.");
    }
}


