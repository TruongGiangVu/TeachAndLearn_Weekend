using CuoiTuan2Api.Models;

// Dto -> Data transfer object

namespace CuoiTuan2Api.Dtos
{
    // Kiểu T này gọi là generic
    public class ResponseDto<T> // generic type -> kiểu dữ liệu chung chung clas dùng để response
    {
        public string ErrCode { get; set; } = string.Empty;
        public string ErrMessage { get; set; } = string.Empty;
        public T? Payload { get; set; } = default;
    }
}
