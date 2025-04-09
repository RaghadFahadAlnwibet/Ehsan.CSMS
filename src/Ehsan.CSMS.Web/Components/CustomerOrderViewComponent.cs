using Ehsan.CSMS.IService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Ehsan.CSMS.Dtos.CustomerDto;
using Ehsan.CSMS.Web.Models.OrderViewModels;
using Ehsan.CSMS.Dtos.OrderDto;
namespace Ehsan.CSMS.Web.Components
{
    public class CustomerOrderViewComponent : ViewComponent
    {
        public ICustomerService _costumerService;
        public CustomerOrderViewComponent(ICustomerService costumerService)
        {
            _costumerService = costumerService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string mobile)
        {

            var customer = await _costumerService.GetByMobile(mobile);
            if(customer == null)
            {
                customer = new CustomerResponse()
                {
                    Name = string.Empty,
                    MobileNumber = mobile
                };
            }
            var customerUpdate = new CustomerUpdateRequest()
            {
                Id = customer.Id,
                Name = customer.Name,
                MobileNumber = customer.MobileNumber,
            };
           return View("loadCustomerData", customerUpdate);
        }
    }
}

