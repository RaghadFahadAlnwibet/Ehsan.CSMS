using Ehsan.CSMS.Dtos;
using Ehsan.CSMS.IRepositories;
using Ehsan.CSMS.IService;
using Ehsan.CSMS.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.Common;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ehsan.CSMS.Web.Controllers
{
    public class CustomerController : Controller
    {
        public ICostumerService _costumerService;
        public CustomerController(ICostumerService costumerService)
        {
            _costumerService = costumerService;
        }
        // GET: CustomerController
        public async Task<ActionResult> Index()
        {
            try 
            {
                var customers = await _costumerService.GetAllAsync();
                CostumerViewModel custumerViewModel = new CostumerViewModel();
                custumerViewModel.costumers = customers;
                return View(custumerViewModel);
            }
            catch(Exception exception)
            {
                throw new InvalidOperationException("An error occurred during the operation.", exception);
            }
        }

        // GET: CustomerController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var customer = await _costumerService.GetbyIdAsync(id);
            return View(customer);
        }
        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: CustomerController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        // save !!
        public async Task<ActionResult> Create(CostumerDto costumerDto)
        {
           
            await _costumerService.AddAsync(costumerDto);
            return RedirectToAction(nameof(Index));

        }
        // GET: CustomerController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try 
            {
                var cosstumer = await _costumerService.GetbyIdAsync(id);
                return View(cosstumer);
            } catch(Exception exception) 
            {
                throw new InvalidOperationException("An error occurred during the operation.", exception);
            }
        }


        // POST: CustomerController/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CostumerDto costumer)
        {
            try
            {
                await _costumerService.UpdateAsync(costumer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: CustomerController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCostumer(int id)
        {
            try
            {
                await _costumerService.DeleteByIdAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> SearchCostumer(CostumerViewModel costumerViewModel)
        {
            if(costumerViewModel.costumerSearchCriteria== null)
                costumerViewModel.costumerSearchCriteria = new CSMS.Models.CostumerSearchCriteria();
           
            var costumers = await _costumerService.SearchAsync(costumerViewModel.costumerSearchCriteria);
            costumerViewModel.costumers = costumers;
            
            return View("Index", costumerViewModel);

        }
    }
}
