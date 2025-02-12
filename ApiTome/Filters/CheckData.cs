using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiTome.Filters;

public class DataFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if(!filterContext.ModelState.IsValid){
            Console.WriteLine("Invalid Model State Bitch!");
            return;
        }

        //bool isModelValid = filterContext.ModelState.IsValid;

        // if (File.Exists(fileUrl))
        // {
        //     string fileContents = File.ReadAllText(fileUrl);
        //     File.Delete(fileUrl);

        //     eventLog.WriteEntry(fileContents, EventLogEntryType.SuccessAudit, 101, 1);
        // }

        // if (isModelValid)
        // {
        //     Log("OnActionExecuting", filterContext.RouteData);
        // }
    }

    // private void Log(string methodName, RouteData routeData)
    // {
    //     var controllerName = routeData.Values["controller"];
    //     var actionName = routeData.Values["action"];
    //     var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
    //     _logger.LogInformation(message, "Action Filter Log");
    // }
}