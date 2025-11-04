using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SPSD.WebApi.Filters
{
    public class GlobalActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //if (DateTime.Now > DateTime.Parse("2026-03-03 00:00:00"))
            //{
            //    //context.Result as ObjectResult

            //    context.Result = new UnauthorizedObjectResult("401")
            //    { 
                    
            //    };
            //}
        }
    }
}
