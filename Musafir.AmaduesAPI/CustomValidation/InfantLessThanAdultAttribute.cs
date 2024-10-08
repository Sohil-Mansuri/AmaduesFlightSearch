﻿using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Resources;

namespace Musafir.AmaduesAPI.CustomValidation
{
    public class InfantLessThanAdultAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is byte infantCount)
            {
                var adultCount = Convert.ToByte(validationContext.ObjectType.GetProperty("Adult")?.GetValue(validationContext.ObjectInstance));

                if (infantCount <= adultCount)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(GetErrorMessage(validationContext));
                }
            }

            return new ValidationResult("Invalid date format");
        }


        private string? GetErrorMessage(ValidationContext validationContext)
        {
            ArgumentNullException.ThrowIfNull(validationContext);

            if (ErrorMessageResourceType != null && !string.IsNullOrEmpty(ErrorMessageResourceName))
            {
                var resourceManager = new ResourceManager(ErrorMessageResourceType);
                return resourceManager.GetString(ErrorMessageResourceName, CultureInfo.CurrentCulture);
            }

            return "Error in validation";
        }
    }
}
