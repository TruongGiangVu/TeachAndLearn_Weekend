namespace CuoiTuan2Api.Models
{
    public class ToDo // database code 
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsDone { get; set; } // true task đã xong, false chưa xong
        public DateTime StartDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}