namespace CuoiTuan1Api.Utils
{
    public class TryParse
    {
        // no ko khai báo
        // static đã lưu bộ nhớ -> gọi và dùng
        public static DateTime? DateParse(object date)
        {
            
            return DateTime.Parse(date.ToString());
        }
    }
}
