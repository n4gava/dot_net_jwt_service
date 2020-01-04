using JwtService.Commons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JwtService.Controllers.Attributes
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var result = new Result();
                result.AddModelStateErrors(context.ModelState);
                context.Result = new BadRequestObjectResult(result);
            }

            base.OnActionExecuting(context);
        }
    }
}
