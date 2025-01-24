using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Ehsan.CSMS.Dtos;
using Ehsan.CSMS.IService;
using System;
using Ehsan.CSMS.Entities;
using Ehsan.CSMS.Web.Models;
using Microsoft.IdentityModel.Tokens;
using DeviceDetectorNET;
using Ehsan.CSMS.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ehsan.CSMS.Models;
using System.Linq;

namespace Ehsan.CSMS.Web.Controllers
{

    public class ProductController : Controller
    {
        public IProductService _IProductService;
        public ICategoryServices _CategoryService;
        public ProductController(IProductService IProductService, ICategoryServices categoryService)
        {
            _IProductService = IProductService;
            _CategoryService = categoryService;
        }
        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            var products = await _IProductService.GetAllAsync();// include 
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.products = products;
            await LoadLookups();
            return View(productViewModel);
        }

        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            await LoadLookups();
            var product = await _IProductService.GetbyIdAsync(id);

            return View(product);
        }

        // GET: ProductController/Create
        public async Task<ActionResult> Create()
        {
           
            await LoadLookups();
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductDto product)
        {
            
               
                    await _IProductService.AddAsync(product);
                    return RedirectToAction(nameof(Index));

                
           
            
              
        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try 
            {
                await LoadLookups();
                var product = await _IProductService.GetbyIdAsync(id);
                return View(product);
            }
            catch(Exception exception)
            {
                throw new InvalidOperationException("An error occurred during the operation.", exception);
            }

        }

        // POST: ProductController/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductDto product)
        {
           
                await _IProductService.UpdateAsync(product);
                return RedirectToAction(nameof(Index));

           
            
               
            
        }

        // GET: ProductController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: ProductController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                await _IProductService.DeletebyIdAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> SearchProduct(ProductViewModel productViewModel)
        {
            if (productViewModel.productSearchCriteria == null)
                productViewModel.productSearchCriteria = new CSMS.Models.ProductSearchCriteria();
            var Filtersproducts = await _IProductService.SearchAsync(productViewModel.productSearchCriteria);
            productViewModel.products = Filtersproducts;
           
            await LoadLookups();
            return View("Index", productViewModel);
        }

        private async Task LoadLookups()// for categoris 
        { 
            var categories = await _CategoryService.GetAllAsync();

             //SelectList sellst = new SelectList(categories, "Id", "CategoryName");
            ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName");

        }
        public async Task<double> GetProductPrice(int id)
        {
            var productPrice = await _IProductService.GetPricePerUnintbyIdAsync(id);
            return productPrice;
        }

    }
}
