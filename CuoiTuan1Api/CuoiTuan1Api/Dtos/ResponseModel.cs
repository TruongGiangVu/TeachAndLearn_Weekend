using CuoiTuan1Api.Models;

// Dto -> Data transfer object

namespace CuoiTuan1Api.Dtos
{
    public class ResponseModel<T> // generic type -> kiểu dữ liệu chung chung clas dùng để response
    {
        public string ErrCode { get; set; } = string.Empty;
        public string ErrMessage { get; set; } = string.Empty;
        public T? Payload { get; set; } = default;
    }
}
