using HonyStore.Domain.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HonyStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Foo : ControllerBase
    {
        [HttpGet] 
        public string Get()
        {
            

            return "Foo";
        }
        public void Save()
        {

        }
    }
}
