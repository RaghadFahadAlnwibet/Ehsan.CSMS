using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Ehsan.CSMS.Constant;

namespace Ehsan.CSMS.Dtos
{
    public class CategoryDto:EntityDto<int>
    {

        [Required(ErrorMessage = "Please enter Category name.")]
        [MaxLength(10)]
        [Display(Name = Fields.CategoryFields.CategoryName)]
        public required String CategoryName { get; set; }

    }  
}
