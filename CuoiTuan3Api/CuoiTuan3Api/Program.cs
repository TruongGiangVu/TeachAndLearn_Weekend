using CuoiTuan3Api.OracleDb;
using CuoiTuan3Api.Services;
using Serilog;
using System.Globalization;
using System.Text.Json.Serialization;

DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

// * Config ISO8601 CultureInfo, với format dd-MM-yyyy HH:mm:ss mặc định
CultureInfo cultureInfo = new("vi-VN"); // chỉnh lại culture thành vietnam
cultureInfo.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd"; // định dạng ngày default
cultureInfo.DateTimeFormat.LongTimePattern = "HH:mm:ss"; // định dạng thời gian default
CultureInfo.DefaultThreadCurrentCulture = cultureInfo; // chỉnh lại trên source
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo; // chỉnh lại trên sourc

// Serilog
// config lại logger của api bằng cách dùng FluentApi
string logTemplate = "[{Timestamp:dd/MM/yyyy HH:mm:ss}] [{Level:u3}] <{ThreadId}> [{SourceContext}] {Message:lj}{NewLine}{Exception}";
Log.Logger = new LoggerConfiguration()
             // đoạn này để log hệ thống không quá nhiều bằng cách giới hạn lại mininum level log
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Routing", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware", Serilog.Events.LogEventLevel.Fatal)
            .Enrich.WithThreadId() // thêm threadId tại trường {ThreadId}
            .Enrich.FromLogContext() // cho phép thêm log context
                                     // chỗ {SourceContext} sẽ log tên class mà ko log namespace của class ví dụ sẽ log "HomeController" thay vì "CuoiTuan3Api.Controllers.HomeController"
            .Enrich.WithComputed("SourceContext", "Substring(SourceContext, LastIndexOf(SourceContext, '.') + 1)") 
            .WriteTo.File(
                path: "Logs/log-.log", // log trong folder Logs, được tạo tự động khi code chạy
                rollingInterval: RollingInterval.Day, // nói chung là loi mỗi ngày
                rollOnFileSizeLimit: true, // khi file tới giới hạn size cho phép -> tạo file mới
                retainedFileCountLimit: null, // số lượng file tạo mới cùng ngày là ko giới hạn
                outputTemplate: logTemplate // template log
            )
            .WriteTo.Console() // ghi log vào console luôn
            .CreateLogger();

builder.Services.AddSerilog(); // đoạn này để add service thì phải
// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDatabaseConnect, DatabaseConnect>();
builder.Services.AddScoped<IToDoService, ToDoService>();

builder.Host.UseSerilog(); // add vào log hệ thống

var app = builder.Build();

// thêm log cho mỗi request tới các endpoint
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
