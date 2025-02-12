using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiTome.Filters;

public class DataFilter : ActionFilterAttribute
{
    private readonly ILogger<DataFilter> _logger;

    public DataFilter(ILogger<DataFilter> logger){
        _logger = logger;
    }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if(_logger.IsEnabled(LogLevel.Information)){
            _logger.LogInformation("Model : \n" + filterContext.HttpContext.ToString());
        }

        bool isModelValid = filterContext.ModelState.IsValid;

        // if (File.Exists(fileUrl))
        // {
        //     string fileContents = File.ReadAllText(fileUrl);
        //     File.Delete(fileUrl);

        //     eventLog.WriteEntry(fileContents, EventLogEntryType.SuccessAudit, 101, 1);
        // }

        if (isModelValid)
        {
            Log("OnActionExecuting", filterContext.RouteData);
        }
    }
    
    private void Log(string methodName, RouteData routeData)
    {
        var controllerName = routeData.Values["controller"];
        var actionName = routeData.Values["action"];
        var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
        _logger.LogInformation(message, "Action Filter Log");
    }
}