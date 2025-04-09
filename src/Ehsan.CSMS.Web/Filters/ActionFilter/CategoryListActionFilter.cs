using Ehsan.CSMS.IService;
using Ehsan.CSMS.Web.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Ehsan.CSMS.Web.Filters.ActionFilter;

public class CategoryListActionFilter : IAsyncActionFilter
{
    private readonly ICategoryServices _categoryService;
    public CategoryListActionFilter(
        ICategoryServices categoryServices)
    {
        _categoryService = categoryServices;
    }
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context, 
        ActionExecutionDelegate next)
    {
        if(context.Controller is ProductController productController)
        {
            var categories = await _categoryService.GetAllAsync();

            productController.ViewBag.CategoryList = 
                new SelectList(categories, "Id", "Name");
        }
        await next();

    }
}
