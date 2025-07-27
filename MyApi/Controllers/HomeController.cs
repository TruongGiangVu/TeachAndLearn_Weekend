using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace MyApi.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{


    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetHome")]
    public async Task<IActionResult> Get([FromQuery] string? search = null)
    {
        // taoj http client
        using HttpClient client = new HttpClient();

        // set dịa chỉ host url 
        client.BaseAddress = new Uri("http://localhost:5271");

        // set header, nếu cần thiết
        client.DefaultRequestHeaders.Add("Authorization", "Bearer your_token_here");

        // Method Get, uri endpoint, gọi api
        HttpResponseMessage response = await client.GetAsync($"/Home/?search={search}");

        // ans = "{"id": 1,"name": "Test fd","summary": ""}"
        string? ans = await response.Content.ReadAsStringAsync();
        Console.WriteLine(ans);
        Console.WriteLine(response.StatusCode.ToString());
        // MyClass? myclass = await response.Content.ReadFromJsonAsync<MyClass>();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        options.Converters.Add(new JsonStringEnumConverter());

        MyClass? myClass = JsonSerializer.Deserialize<MyClass>(ans, options);
        myClass.Oracle = "My Oracle";
        Console.WriteLine("myClass: " + JsonSerializer.Serialize(myClass, options));

        return Ok(myClass.Name);
    }
}
public enum Status
{
    None,
    Done,
}

public enum ErrCode
{
    Success = 0,
    Fail = 99,
}

public class MyClass
{
    public ErrCode ErrCode { get; set; } = ErrCode.Success;
    public int Id { get; set; } = 0;
    public string Name { get; set; } = string.Empty;
    public string? Oracle { get; set; } = string.Empty;
    public Status? Status { get; set; }
}


// ErrCode: "00"