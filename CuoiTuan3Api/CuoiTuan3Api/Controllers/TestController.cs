using CuoiTuan3Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using CuoiTuan3Api.Constants;

namespace CuoiTuan3Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public TestController(
            ILogger<TestController> logger,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory
        )
        {
            _logger = logger;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        [HttpPost]
        public IActionResult Index([FromBody] MyClass input)
        {
            input.Name = input.Name + " test";
            Enum.TryParse<Status>("Procesing", true, out Status ans);
            //input.Status = ans;

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
            string connectString = _configuration.GetValue<string>("AppSettings:ConnectionString") ?? string.Empty;
            string IBFTameAAAA = "fdsfs"; // ACCOUNT_GROUP_CODE 
            string ACCOUNT_GROUP_CODE = string.Empty;
            return Ok(new {a, connectString, IBFTameAAAA, ACCOUNT_GROUP_CODE });
        }

        [HttpGet("client")]
        public async Task<IActionResult> ClientAsync() 
        {

            // model
            MyClass reqBody = new MyClass();
            reqBody.Name = "giang sieu nh";
            reqBody.Status = "DONE";
            reqBody.Summary = "Summary";

            // serialize 
            string json = JsonSerializer.Serialize(reqBody);
            // thêm cái json vào StringContent/ Content-Type: application/json
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            
            // tạo httpClinet
            using HttpClient client = new HttpClient();

            // set dịa chỉ host url 
            client.BaseAddress = new Uri("http://localhost:5271");

            // set header, nếu cần thiết
            client.DefaultRequestHeaders.Add("Authorization", "Bearer your_token_here");

            // Method Get, uri endpoint, gọi api
            HttpResponseMessage response = await client.PostAsync($"/Home", content);

            // ans = "{"id": 1,"name": "Test fd","summary": ""}"
            string? ans = await response.Content.ReadAsStringAsync();
            MyClass? resBody = await response.Content.ReadFromJsonAsync<MyClass>();
            return Ok(resBody);
        }

        [HttpGet("client2")]
        public async Task<IActionResult> Client2Async()
        {

            // tạo requestBody content model
            MyClass reqBody = new MyClass();
            reqBody.Name = "giang sieu nh";
            reqBody.Status = "DONE";
            reqBody.Summary = "Summary";

            // tạo httpClient
            using HttpClient client = new HttpClient();

            // set dịa chỉ host url 
            client.BaseAddress = new Uri("http://localhost:5271");

            // set header, nếu cần thiết
            client.DefaultRequestHeaders.Add("Authorization", "Bearer your_token_here");

            // Method Get, uri endpoint, gọi api
            HttpResponseMessage response = await client.PostAsJsonAsync($"/Home", reqBody);

            // ans = "{"id": 1,"name": "Test fd","summary": ""}"
            string? ans = await response.Content.ReadAsStringAsync();
            MyClass? resBody = await response.Content.ReadFromJsonAsync<MyClass>();
            return Ok(resBody);
        }

        [HttpGet("GetHome")]
        public async Task<IActionResult> Get([FromQuery] string? search = null)
        {
            // taoj http client
            using HttpClient client = new HttpClient();

            // set dịa chỉ host url 
            client.BaseAddress = new Uri("http://localhost:5271");

            // set header, nếu cần thiết
            client.DefaultRequestHeaders.Add("Authorization", "Bearer your_token_here");

            // Method Get, uri endpoint, gọi api
            HttpResponseMessage response = await client.GetAsync($"/Home/?search={Uri.EscapeDataString(search)}");

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
            Console.WriteLine("myClass: " + JsonSerializer.Serialize(myClass, options));

            return Ok(myClass.Name);
        }

        [HttpGet("GetHome2")]
        public async Task<IActionResult> Get2([FromQuery] string? search = null)
        {
            // taoj http client
            using HttpClient client = _httpClientFactory.CreateClient(AppConstant.Party1Api);
            //using HttpClient client2 = _httpClientFactory.CreateClient("Api2");

            // set header, nếu cần thiết
            client.DefaultRequestHeaders.Add("Authorization", "Bearer your_token_here");

            // Method Get, uri endpoint, gọi api
            HttpResponseMessage response = await client.GetAsync($"Home/?search={Uri.EscapeDataString(search)}");

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
            Console.WriteLine("myClass: " + JsonSerializer.Serialize(myClass, options));

            return Ok(myClass.Name);
        }
    }
}

public class MyClass
{
    public int Id { get; set; } = 0;
    public string Name { get; set; } = string.Empty;
    public string Summary { get; set; }
    public string Status { get; set; } // None, Done
}