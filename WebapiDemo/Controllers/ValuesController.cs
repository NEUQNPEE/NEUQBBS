using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiDemo
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public string GetA(string name, string helloWord)
        {
            if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(helloWord))
            {
                return "Hello World!";
            }
            return $"{helloWord} {name}!";
        }
    }
}
