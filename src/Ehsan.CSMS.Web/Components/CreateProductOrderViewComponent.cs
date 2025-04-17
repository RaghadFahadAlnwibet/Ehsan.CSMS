using Ehsan.CSMS.Dtos.OrderDetailsDto;
using Ehsan.CSMS.IService;
using Ehsan.CSMS.Web.Models.OrderViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace Ehsan.CSMS.Web.Components;

public class CreateProductOrderViewComponent : ViewComponent
{
    public IProductService _ProductService;

    public CreateProductOrderViewComponent(IProductService productService)
    {
        _ProductService = productService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        CreateOrderViewModel createOrderViewModel = new CreateOrderViewModel();
        
        var products = await _ProductService.GetAllAsync();
        createOrderViewModel.Products = products;
        return View("LoadProductData", createOrderViewModel);
    }
}
