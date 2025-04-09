using Ehsan.CSMS.IService;
using Ehsan.CSMS.Web.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Ehsan.CSMS.Web.Filters.ActionFilter;

public class CatchierListActionFilter : IAsyncActionFilter
{
    private readonly ICashierService _cashierService;
    public CatchierListActionFilter(ICashierService cashierService)
    {
        _cashierService = cashierService;
    }
    public async Task OnActionExecutionAsync
        (ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        if(context.Controller is OrderController orderController)
        {
            var cashiers = await _cashierService.GetAllAsync();
            orderController.ViewBag.Cashiers = new SelectList(cashiers, "Id", "Name");
        }
        await next();
    }
}
