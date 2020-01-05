using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtService.Commons
{
    public static class ResultExtensions
    {
        public static Result<T1> Add<T1, T2>(this Result<T1> result, Result<T2> resultWithErrors)
        {
            foreach (var error in resultWithErrors.Errors)
            {
                result.AddError(error);
            }

            return result;
        }

        public static Result<T> Add<T>(this Result<T> result, string errorMessage)
        {
            result.AddError(errorMessage);
            return result;
        }

        public static Result<T> Add<T>(this Result<T> result, Exception exception)
        {
            result.AddError(exception.Message);
            return result;
        }
        public static Result<T> Add<T>(this Result<T> result, IEnumerable<string> errorMessages)
        {
            foreach (var errorMessage in errorMessages)
            {
                result.AddError(errorMessage);
            }

            return result;
        }

        public static Result<T> AddModelStateErrors<T>(this Result<T> result, ModelStateDictionary modelStateDictionary)
        {
            if (!modelStateDictionary.IsValid)
            {
                var modelErrors = modelStateDictionary.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage);
                result.Add(modelErrors);
            }

            return result;
        }
    }
}
