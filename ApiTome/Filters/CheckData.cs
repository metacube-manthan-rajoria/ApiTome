using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiTome.Filters;

public class DataFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if(!filterContext.ModelState.IsValid){
            Console.WriteLine("Invalid Model State Bro!");
            return;
        }
    }
}