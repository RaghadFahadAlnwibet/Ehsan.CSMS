using Microsoft.AspNetCore.Http;
using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Mvc;
using Ehsan.CSMS.Dtos;
using Ehsan.CSMS.Models;
using Ehsan.CSMS.IService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Ehsan.CSMS.Web.Models;
using Ehsan.CSMS.IRepositories;
using Volo.Abp.Domain.Repositories;
using Ehsan.CSMS.Entities;

namespace Ehsan.CSMS.Web.Controllers
{
    public class CategoryController : Controller
    {
        public ICategoryServices _IcategoryServices;
        public CategoryController(ICategoryServices categoryServices)
        {
            _IcategoryServices = categoryServices;
        }

        // GET: CategoryController
        public async Task<ActionResult> Index()
        {
            var categories = await _IcategoryServices.GetAllAsync();
            CategoryViewModel categoryViewModel = new CategoryViewModel();
            categoryViewModel.categories = categories;
            return View(categoryViewModel);
        }

        // GET: CategoryController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try {
                var category = await _IcategoryServices.GetbyIdAsync(id);
                return View(category);
            }
            catch(Exception exception) 
            {

                throw new InvalidOperationException("An error occurred during the operation.", exception);
            }
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryDto category)
        {
            try
            {
                await _IcategoryServices.AddAsync(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var category = await _IcategoryServices.GetbyIdAsync(id);
            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CategoryDto category)
        {
            try
            {
                await _IcategoryServices.UpdateAsync(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: CategoryController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                await _IcategoryServices.DeleteByIdAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> SearchCategory(CategoryViewModel categoryViewModel)
        {
            if (categoryViewModel.categorySearchCriteria == null)
                categoryViewModel.categorySearchCriteria = new CSMS.Models.CategorySearchCriteria();

            var categories = await _IcategoryServices.SearchAsync(categoryViewModel.categorySearchCriteria);
            categoryViewModel.categories = categories;
                
            return View("Index", categoryViewModel);
         
        }



    }
}
