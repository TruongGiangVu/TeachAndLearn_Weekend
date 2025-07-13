namespace CuoiTuan2Api.Utils
{
    public class TryParse
    {
        // no ko khai báo
        // static đã lưu bộ nhớ -> gọi và dùng
        // function dùng
        public static DateTime? DateParse(object date)
        {
            return DateTime.Parse(date.ToString());
        }

        public static bool Boolean(object value) {
            string valueStr = value.ToString();
            bool result = false;
            //if (valueStr == "1")
            if (valueStr == "Y") 
            {
                result = true;
            }
            return result;
            //reader["is_done"].ToString() == "Y" ? true : false
        }
    }
}
