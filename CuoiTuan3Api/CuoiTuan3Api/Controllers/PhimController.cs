using CuoiTuan3Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CuoiTuan3Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhimController : ControllerBase
    {
        [HttpPost]
        public IActionResult Index([FromBody] Phim input)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new JsonStringEnumConverter());

            Console.WriteLine("input: " + JsonSerializer.Serialize(input, options));

            // oracle
            string phimType = "Action"; // 2
            // string -> enum
            Enum.TryParse<PhimType>(phimType, true, out PhimType result);
            Phim phim = new Phim();
            phim.Id = 1;
            phim.Name = "Test";
            phim.Type = result;
            return Ok(phim);
        }
    }
}
