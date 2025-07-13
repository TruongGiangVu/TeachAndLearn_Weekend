namespace CuoiTuan2Api.Utils
{
    public class Handler
    {
        public static DateTime? SetToZeroTime(DateTime? input)
        {
            DateTime? ans = null;
            if (input is not null)
                ans = new DateTime(input.Value.Year, input.Value.Month, input.Value.Day, 0, 0, 0);
            return ans;
        }

        public static DateTime? SetToEndTime(DateTime? input)
        {
            DateTime? ans = null;
            if (input is not null)
                ans = new DateTime(input.Value.Year, input.Value.Month, input.Value.Day, 23, 59, 59);
            return ans;
        }
    }
}
