using Ehsan.CSMS.EntityFrameworkCore;
using Ehsan.CSMS.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ehsan.CSMS.Web.Components
{
    public class CustomerViewComponent : ViewComponent
    {
        public ICostumerService _costumerService;
        public CustomerViewComponent(ICostumerService costumerService)
        {
            _costumerService = costumerService;
        }
        public async Task<IViewComponentResult> InvokeAsync( string mobile)
        {
            
            var customer = await _costumerService.GetCustomerByMobileNumberAsync(mobile);
            return View(customer);
        }
    }
}
