using Ehsan.CSMS.Dtos;
using Ehsan.CSMS.Entities;
using Ehsan.CSMS.Models;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.Options;
using System.Collections.Generic;
using Ehsan.CSMS.Enums;
using System.ComponentModel.DataAnnotations;
using Ehsan.CSMS.Constant;
using System;
namespace Ehsan.CSMS.Web.Models
{
    public class OrderViewModel
    {
        public OrderSearchCriteria orderSearchCriteria { get; set; }
        public List<OrderDto> orders { get; set; }


        public OrderDto order { get; set; }

    }
}




