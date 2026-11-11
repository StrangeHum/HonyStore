using Microsoft.AspNetCore.Mvc;

namespace HonyStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "GET работает!";
        }
    }
}