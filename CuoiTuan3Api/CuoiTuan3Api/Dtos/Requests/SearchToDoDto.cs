using System.ComponentModel.DataAnnotations;

namespace CuoiTuan3Api.Dtos.Requests
{
    public class SearchToDoDto
    {
        [Required]
        public string? Name { get; set; } = string.Empty;
        public string? Status { get; set; } = string.Empty;
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
