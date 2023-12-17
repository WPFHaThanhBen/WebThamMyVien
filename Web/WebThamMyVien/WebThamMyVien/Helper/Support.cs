using System.Security.Cryptography;
using System.Text;

namespace WebThamMyVien.Helper
{
    public class Support
    {
        public string ConvertDateFormatIn(string yyyymmdd)
        {
            // Chuyển đổi ngày từ định dạng "yyyy-MM-dd" sang "dd-MM-yyyy"
            DateTime date = DateTime.ParseExact(yyyymmdd, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            return date.ToString("dd-MM-yyyy");
        }
        public string ConvertDateFormatOut(string ddmmyyyy)
        {
            // Chuyển đổi ngày từ định dạng "dd-MM-yyyy" sang "yyyy-MM-dd"
            DateTime date = DateTime.ParseExact(ddmmyyyy, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            return date.ToString("yyyy-MM-dd");
        }

        // Hàm lấy giờ:phút:giây hiện tại
        public static string GetCurrentTime()
        {
            DateTime now = DateTime.Now;
            return now.ToString("HH:mm:ss");
        }

        // Hàm lấy ngày/tháng/năm hiện tại
        public static string GetCurrentDate()
        {
            DateTime now = DateTime.Now;
            return now.ToString("dd/MM/yyyy");
        }

        public static string GetCurrentDateTime()
        {
            return GetCurrentTime() + " " + GetCurrentDate();
        }
        public static String HmacSHA256(string inputData, string key)
        {
            byte[] keyByte = Encoding.UTF8.GetBytes(key);
            byte[] messageBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                string hex = BitConverter.ToString(hashmessage);
                    hex = hex.Replace("-", "").ToLower();
                return hex;
            }

        }
    }
}
