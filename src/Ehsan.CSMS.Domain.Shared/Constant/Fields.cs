using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ehsan.CSMS.Enums;

namespace Ehsan.CSMS.Constant
{
    public struct Fields
    {
        public struct CashierFields
        {
           public const string CashierName = "Cashier Name";
        }
        public struct CategoryFields
        {
            public const string CategoryName = "Category Name";
        }
        public struct CostumerFields
        {
            public const string CustomerId = "Customer Id";
            public const string CustomerName = "Customer Name";
            public const string ContactInfo = "Mobile Number";
            public const string LoyaltyPoints = "Loyalty Points";
        }
        public struct OrderFields
        {
            public const string OrderId = "Orders";
            public const string OrderStatus = "Order Status";
            public const string TotalPrice = "Total Price";
            public const string NetPrice = "NetPrice";
            public const string discount = "Discount";
            public const string selectProduct = "Select Product";
        }
        public struct ProductFields
        {
            public const string ProductName = "Product Name";
            public const string ProductPrice = "Product Price";
        }
        public struct OrderDetailFields
        {
            public const string TotalPrice = "Total Price";
            public const string PricePerUnit = "Price Per Unit";
            public const string Quantity = "Quantity";
        }



    }
}
