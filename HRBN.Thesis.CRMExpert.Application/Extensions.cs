using System.Collections.Generic;
using HRBN.Thesis.CRMExpert.Application.Core;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HRBN.Thesis.CRMExpert.Application
{
    public static class Extensions
    {
        public static void PopulateValidation(this ModelStateDictionary modelState, IEnumerable<Result.Error> errors)
        {
            foreach (var error in errors)
            {
                modelState.AddModelError(error.PropertyName, error.Message);
            }
        }   
        
        public static void PopulateViewModelValidation(this ModelStateDictionary modelState, IEnumerable<Result.Error> errors, string prefix)
        {
            foreach (var error in errors)
            {
                modelState.AddModelError($"{prefix}.{error.PropertyName}", error.Message);
            }
        }  
    }
}