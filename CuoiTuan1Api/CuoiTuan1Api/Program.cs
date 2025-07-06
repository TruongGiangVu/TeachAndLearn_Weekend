using CuoiTuan1Api.OracleDb;
using CuoiTuan1Api.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// * Config ISO8601 CultureInfo, với format dd-MM-yyyy HH:mm:ss mặc định
CultureInfo cultureInfo = new("vi-VN"); // chỉnh lại culture thành vietnam
cultureInfo.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd"; // định dạng ngày default
cultureInfo.DateTimeFormat.LongTimePattern = "HH:mm:ss"; // định dạng thời gian default
CultureInfo.DefaultThreadCurrentCulture = cultureInfo; // chỉnh lại trên source
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo; // chỉnh lại trên sourc



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DatabaseConnect>();
builder.Services.AddScoped<ToDoService>();

var app = builder.Build();

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
