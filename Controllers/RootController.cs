using Microsoft.AspNetCore.Mvc;

namespace Sukalibur.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RootController : ControllerBase
    {
        private readonly ILogger<RootController> _logger;

        public RootController(ILogger<RootController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Hello")]
        public IActionResult Hello()
        {
            return new JsonResult(new { message = "Hello, World!" });
        }
    }
}
