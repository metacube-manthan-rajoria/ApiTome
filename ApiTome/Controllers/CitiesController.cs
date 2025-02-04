using Microsoft.AspNetCore.Mvc;

namespace ApiTome.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase {
    [HttpGet]
    public string Method(){
        return "wadawd";
    }
}