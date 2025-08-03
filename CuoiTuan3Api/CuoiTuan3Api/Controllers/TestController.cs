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
        private readonly IConfiguration _configuration;

        public TestController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
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

        [HttpGet("test2")]
        public IActionResult Index2()
        {
            string a = Environment.GetEnvironmentVariable("GIANG_VAR_1") ?? string.Empty;
            string b = _configuration.GetValue<string>("AppSettings:ConnectionString") ?? string.Empty;
            return Ok(new {a, b});
        }
    }
}
