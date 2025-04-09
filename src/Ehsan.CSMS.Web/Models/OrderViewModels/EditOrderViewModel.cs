using Ehsan.CSMS.Dtos.OrderDetailsDto;
using Ehsan.CSMS.Dtos.ProductDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ehsan.CSMS.Web.Models.OrderViewModels;

public class EditOrderViewModel 
{
    // child class => presentation logic 
    public Guid SelectedProduct { get; set; }
    public double PricePerUnit { get; set; } 

    public int Quantity { get; set; } 
    public double SubTotals { get; set; } 

    //public OrderDetailsAddRequest OrderDetails { get; set; }
    // TomList
    public IEnumerable<ProductResponse>? Products { get; set; }

    // because i use js the filter pipeline will not be entered
    //public IEnumerable<ValidationResult> Validate
    //    (ValidationContext validationContext)
    //{
    //    List<ValidationResult> results = new List<ValidationResult>();
    //    if (SelectedProduct == null || SelectedProduct.Count == 0)
    //    {
    //        results.Add(new ValidationResult("products list has 0 count"));
    //        yield return new ValidationResult("Please select at least one product");
    //    }
    //    if (Quantity == null || Quantity.Count == 0)
    //    {
    //        results.Add(new ValidationResult("quantity list has 0 count"));
    //        yield return new ValidationResult("Please enter a product quantity");
    //    }
    //    // three products in product list must have thre selected quantity 
    //    if (SelectedProduct != null && Quantity != null)
    //    {
    //        if (SelectedProduct.Count != Quantity.Count)
    //        {
    //            results.Add(new ValidationResult("quantity list has and products list mismatch"));
    //            yield return new ValidationResult("Mismatch: Each selected product must have a corresponding quantity.", 
    //                new[] { nameof(SelectedProduct), nameof(Quantity)});
    //        }
    //    }
    //}
}
