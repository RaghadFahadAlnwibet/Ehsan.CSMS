using Ehsan.CSMS.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Ehsan.CSMS.Web.Models
{
    public class CreateOrderViewModel
    {
        // child class 
        public OrderDto order { get; set; }

        public List<int> selectedPrpducts { get; set; }

        public List<decimal> pricePerUnit { get; set; }

        public List<int> qunatity { get; set; }

        public List<int> subTotals { get; set; }

        public List<ProductDto> products { get; set; }


    }
}
