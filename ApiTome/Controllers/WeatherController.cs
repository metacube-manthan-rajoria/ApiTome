using Microsoft.AspNetCore.Mvc;

namespace ApiBasicsService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase {
    [HttpGet]
    public string Method(){
        return "wadawd";
    }
}