using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ehsan.CSMS.Constant;
using System.ComponentModel.DataAnnotations;
using Ehsan.CSMS.Enums;
using Volo.Abp.Application.Dtos;

namespace Ehsan.CSMS.Dtos
{
    public class ProductDto : EntityDto<int>
    {

        [Required(ErrorMessage = "Please select a product category.")]
        [Display(Name = "Category name")]
        public required int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter Product name.")]
        [MaxLength(20)]
        [Display(Name = Fields.ProductFields.ProductName)]
        public required string ProductName { get; set; }

        [Required(ErrorMessage = "Please select Product price.")]
        [Display(Name = Fields.ProductFields.ProductPrice)]
        [Range(15, 100)]
        public required double ProductPrice { get; set; }
        public CategoryDto Category { get; set; }

    }
}
