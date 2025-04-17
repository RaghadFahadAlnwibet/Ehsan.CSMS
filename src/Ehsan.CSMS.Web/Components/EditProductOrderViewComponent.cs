using Ehsan.CSMS.Dtos.OrderDetailsDto;
using Ehsan.CSMS.IService;
using Ehsan.CSMS.Web.Models.OrderViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace Ehsan.CSMS.Web.Components;

public class EditProductOrderViewComponent : ViewComponent
{
    public IProductService _ProductService;

    public EditProductOrderViewComponent(IProductService productService)
    {
        _ProductService = productService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        EditOrderViewModel editOrderViewModel = new EditOrderViewModel();
        
        var products = await _ProductService.GetAllAsync();
        editOrderViewModel.Products = products;
        return View("LoadProductData", editOrderViewModel);
    }
}
