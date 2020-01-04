using JwtService.Commons.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtService.Controllers.Attributes
{
    public class ResultRequestAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context?.Result is ObjectResult)
            {
                var objectResult = (ObjectResult)context.Result;
                if (objectResult.Value is IResult)
                {
                    var result = (IResult)objectResult.Value;
                    
                    if (result.Failed)
                        context.Result = new BadRequestObjectResult(result);
                    else
                        context.Result = new OkObjectResult(result);
                }
            }

            base.OnActionExecuted(context);
        }
    }
}
