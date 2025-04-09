using Ehsan.CSMS.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace Ehsan.CSMS.Web.Filters.ActionFilter;

public class ModelValidatorActionFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context, 
        ActionExecutionDelegate next)
    {
        if (context.Controller is Controller controller)
        {
            if(context.HttpContext.Request.Method == "POST" || 
                context.HttpContext.Request.Method == "UPDATE")
            if (!controller.ModelState.IsValid)
            {
                controller.ViewBag.Errors = controller.ModelState
                    .Values
                    .SelectMany(value => value.Errors)
                    .Select(e => e.ErrorMessage);
                context.Result = controller.View();
            }
            else
            {
                await next();
            }
        }
    }
}
