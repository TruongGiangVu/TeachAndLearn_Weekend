using CuoiTuan3Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace CuoiTuan3Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpPost]
        public IActionResult Index([FromBody] MyClass input)
        {
            input.Name = input.Name + " test";
            Enum.TryParse<Status>("Procesing", true, out Status ans);
            input.Status = ans;

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: true));
            Console.WriteLine(JsonSerializer.Serialize(input, options));

            return Ok(input);
        }
    }
}
