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
    }
}
