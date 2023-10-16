namespace APICosmeticClinic.Helper
{
    public class Support
    {
        public string GetCurrentDate()
        {
            DateTime currentDate = DateTime.Now;
            return currentDate.ToString("dd/MM/yyyy");
        }
    }
}
