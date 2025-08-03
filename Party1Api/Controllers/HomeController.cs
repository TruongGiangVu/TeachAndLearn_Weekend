using Microsoft.AspNetCore.Mvc;

namespace Party1Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    [HttpGet]
    public IActionResult Get([FromQuery] string? search = null, [FromQuery] string? name = null)
    {
        Console.WriteLine("serach: " + search);
        MyClass res = new MyClass
        {
            Id = 1,
            Name = "Test " + search,
            Summary = "",
            Status = "Done"
        };
        return Ok(res);
    }

    [HttpPost]
    public IActionResult Post([FromBody] MyClass input)
    {
        MyClass res = new MyClass
        {
            Id = input.Id +1,
            Name = "Test " + input.Name,
            Summary = input.Summary,
            Status = "Done"
        };
        return Ok(res);
    }
}

public class MyClass
{
    public int Id { get; set; } = 0;
    public string Name { get; set; } = string.Empty;
    public string Summary { get; set; }
    public string Status { get; set; } // None, Done
}