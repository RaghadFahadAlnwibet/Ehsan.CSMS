using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Ehsan.CSMS.Web.Filters.ActionFilter;

public class SearchSelectionActionFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context, 
        ActionExecutionDelegate next)
    {
        if(context.Controller is Controller controller)
        {
            if (context.ActionArguments.ContainsKey("viewModel") 
                && context.ActionArguments["viewModel"] != null)
            {
                // Get type of viewModel
                Type type = context.ActionArguments["viewModel"]!.GetType();
                // Get declared properties
                Type? searchCriteriaType = type.GetProperties()
                    .FirstOrDefault(property => property.Name == "SearchCriteria")?.PropertyType;
                
                controller.ViewBag.SearchSelection = searchCriteriaType?.GetProperties()
                    .Select(property =>
                    new SelectListItem
                    {
                        Value = property.Name, // this is the name of property in which the value of input will be binded with
                        Text = property.GetCustomAttribute<DisplayAttribute>()?.Name ?? property.Name
                    });



                // get value of the properties ............ not need 
                // value of the selection for each list item is the name of the selction property 
                // 

                // store the selection from search critirie 
                // study ActionArguments["SearchCriteria"] 
            }
           
            await next();
            
        }
        // name => selected one, from input => value 
    }
}
