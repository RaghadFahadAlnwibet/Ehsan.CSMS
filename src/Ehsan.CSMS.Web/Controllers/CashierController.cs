using Ehsan.CSMS.Dtos;
using Ehsan.CSMS.IService;
using Ehsan.CSMS.Models;
using Ehsan.CSMS.Web.Models;
using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YamlDotNet.Core.Tokens;

namespace Ehsan.CSMS.Web.Controllers
{
    public class CashierController : Controller
    {
        public  ICashierService _CashierDtoService;

        public CashierController(ICashierService CashierDtoService)
        {
            _CashierDtoService =  CashierDtoService;
        }
        // GET: CashierController
        public async Task<ActionResult> Index()
        {
            var cashiers = await _CashierDtoService.GetAllAsync();
            CashierViewModel cashierViewModel = new CashierViewModel();
            cashierViewModel.Cashiers = cashiers;
          

            return View(cashierViewModel);
        }

        // GET: CashierController/Details/5
        public async  Task<ActionResult> Details(int id)
        {
            var cashier = await _CashierDtoService.GetbyIdAsync(id);
            return View(cashier);
        }

        // GET: CashierController/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: CashierController/Create
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CashierDto casherdto)
        {
            try
            {
                    await _CashierDtoService.AddAsync(casherdto);
                    return RedirectToAction(nameof(Index));   
            }
            catch
            {
                return View();
            }
        }

        // GET: CashierController/Edit/5
        public async Task<ActionResult>  Edit(int id)
        {
            var cashier = await _CashierDtoService.GetbyIdAsync(id);
            return View(cashier);
        }

        // POST: CashierController/Edit/5
        [HttpPost]
       
        public async Task<ActionResult> Edit(CashierDto ashierDto)
        {
            try
            {
                 await _CashierDtoService.UpdateAsync(ashierDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CashierController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: CashierController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCashier(int id)
        {
            try
            {
                await _CashierDtoService.DeleteByIdAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
        public async Task<ActionResult> SearchCashier(CashierViewModel cashierViewModel)
        {
            if (cashierViewModel.SearchCriteria == null)
                cashierViewModel.SearchCriteria = new CSMS.Models.CashierSearchCriteria();

            var cashiers= await _CashierDtoService.SearchAsync(cashierViewModel.SearchCriteria);
            cashierViewModel.Cashiers = cashiers;
            return View("Index", cashierViewModel);
                        
        }


        public async Task<string> getCashierNameById(int id)
        {
           return await _CashierDtoService.GetCashierNamebyId(id);
        }

       




    }
}


