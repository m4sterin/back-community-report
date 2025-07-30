using back_community_report.Extensions.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace back_community_report.Extensions.Filters
{
    public class ApiValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new ApiBadRequestResponse(context.ModelState));
            }

            base.OnActionExecuting(context);
        }
    }
}