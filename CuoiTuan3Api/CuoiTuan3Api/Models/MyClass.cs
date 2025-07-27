namespace CuoiTuan3Api.Models
{
    public class MyClass
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Status Status { get; set; }
    }

    public enum Status
    {
        None =0,
        Success =1,
        Fail =2,
        Procesing = 3,
    }
}
