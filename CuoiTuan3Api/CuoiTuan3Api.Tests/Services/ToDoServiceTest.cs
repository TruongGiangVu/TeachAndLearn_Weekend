using CuoiTuan3Api.Models;
using CuoiTuan3Api.OracleDb;
using CuoiTuan3Api.Services;
using Moq;
using System.Text.Json;

namespace CuoiTuan3Api.Tests.Services
{
    public class ToDoServiceTest
    {
        [Fact]
        public void GetToDoById_IdNull_02_Required()
        {
            // Arrange
            var mockRepo = new Mock<IDatabaseConnect>();

            var service = new ToDoService(mockRepo.Object);

            // Act
            var result = service.GetToDoById(null);
            Console.WriteLine(JsonSerializer.Serialize(result));

            // Assert
            Assert.NotNull(result);
            Assert.Equal("02", result.ErrCode);
        }
        [Fact]
        public void GetToDoById_Id1_Found()
        {
            // Arrange
            var mockRepo = new Mock<IDatabaseConnect>();
            mockRepo.Setup(r => r.GetToDoId(1)).Returns(new ToDo { Id = 1, Name = "Alice" });

            var service = new ToDoService(mockRepo.Object);

            // Act
            var result = service.GetToDoById(1);
            Console.WriteLine(JsonSerializer.Serialize(result));

            // Assert
            Assert.NotNull(result);
            Assert.Equal("00", result.ErrCode);
            Assert.Equal("Alice", result.Payload?.Name);
        }

    }
}
