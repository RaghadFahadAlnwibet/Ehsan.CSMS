using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Ehsan.CSMS.Helper;

internal class ValidationHelper
{
    internal static void ValidateModel(object obj)
    {
        ValidationContext validationContext = new ValidationContext(obj);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject
            (obj, validationContext, validationResults, true);
        if (!isValid)
        {
            throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
        }
    }

}






