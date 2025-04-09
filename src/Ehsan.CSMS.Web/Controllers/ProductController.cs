using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Ehsan.CSMS.IService;
using System;
using Ehsan.CSMS.Models;
using Ehsan.CSMS.Web.Models.ProductViewModel;
using Ehsan.CSMS.Web.Filters.ActionFilter;
using Ehsan.CSMS.Dtos.ProductDto;
namespace Ehsan.CSMS.Web.Controllers;


public class ProductController : Controller
{
    private readonly IProductService _productService;
    public ProductController(
        IProductService productService)
    {
        _productService = productService;
    }
    // GET: ProductController
    [TypeFilter(typeof(CategoryListActionFilter))]
    public async Task<ActionResult> Index(ProductViewModel productViewModel)
    {
        if (productViewModel.ProductSearchCriteria is null)
        {
            productViewModel.ProductSearchCriteria = new ProductSearchCriteria();
        }
        productViewModel.Products = await _productService.SearchAsync(productViewModel.ProductSearchCriteria);
        return View(productViewModel);
    }

    // GET: ProductController/Details/5
    [TypeFilter(typeof(CategoryListActionFilter))]
    public async Task<ActionResult> Details(Guid? id)
    {
        var product = await _productService.GetByIdAsync(id);
        if(product is null)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // GET: ProductController/Create
    [TypeFilter(typeof(CategoryListActionFilter))]
    public async Task<ActionResult> Create()
    {
        return View();
    }

    //// POST: ProductController/Create
    [HttpPost]
    [TypeFilter(typeof(ModelValidatorActionFilter))]
    public async Task<ActionResult> Create(ProductAddRequest? productRequest)
    {
        await _productService.AddAsync(productRequest);
        return RedirectToAction(nameof(Index));
    }

    //// GET: ProductController/Edit/5
    [TypeFilter(typeof(CategoryListActionFilter))]
    public async Task<ActionResult> Edit(Guid? id)
    {
        var product = await _productService.GetByIdAsync(id);
        if(product is null)
        {
            return RedirectToAction(nameof(Index));
        }
        var productUpdateRequest = new ProductUpdateRequest()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            CategoryId = product.CategoryId,
        };
        return View(productUpdateRequest);
    }

    //// POST: ProductController/Edit/5
    [HttpPost]
    [TypeFilter(typeof(ModelValidatorActionFilter))]
    public async Task<ActionResult> Edit(ProductUpdateRequest? productRequest)
    {
        await _productService.UpdateAsync(productRequest);
        return RedirectToAction(nameof(Index));
    }

    public async Task<ActionResult> Delete(Guid? id)
    {
        var isDeleted = await _productService.DeleteByIdAsync(id);
        if (isDeleted)
        {
            return RedirectToAction(nameof(Index));
        }
        
        return BadRequest("Failed to delete product");
        
    }
    public IActionResult GetProductInformation()
    {
        return ViewComponent("ProductOrder");
    }


    public async Task<double> GetProductPrice(Guid id)
    {
        var productPrice = await _productService.GetPriceById(id);
        return productPrice.Value;
    }

}
