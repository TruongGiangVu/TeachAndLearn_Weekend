using CuoiTuan3Api.Models;
using CuoiTuan3Api.OracleDb;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CuoiTuan3Api.Tests.OracleDb
{
    public class DatabaseConnectTest
    {
        private readonly IConfiguration _configuration;

        public DatabaseConnectTest()
        {
            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            _configuration = configBuilder.Build();
        }

        [Fact]
        public void ConnectionString_GetToDoById()
        {
            // Arrange
            int id = 1;
            var dbConnect = new DatabaseConnect(_configuration);

            // Act
            var toDo = dbConnect.GetToDoId(id);
            Console.WriteLine(JsonSerializer.Serialize(toDo));

            // Assert
            Assert.NotNull(toDo);
            Assert.Equal(id, toDo.Id);
            
            
        }
    }
}
