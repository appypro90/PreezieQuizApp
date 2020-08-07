using Microsoft.AspNetCore.Mvc;

namespace PreezieQuizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetHealth() => Ok("Healthy");
    }
}