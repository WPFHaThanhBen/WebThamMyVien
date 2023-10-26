using Microsoft.AspNetCore.Mvc;

namespace WebThamMyVien.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            // Đặt giá trị int vào cookie
            Response.Cookies.Append("IdUser", "1" , new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30) // Thời hạn 30 ngày
            });

            // Lấy giá trị int từ cookie (nếu tồn tại)
            int IdUser = 0;
            if (Request.Cookies.TryGetValue("IdUser", out string intString))
            {
                if (int.TryParse(intString, out int myIntValue))
                {
                    IdUser = myIntValue;
                }
            }

            TempData["menu"] = "Home";
            return View();
        }
    }
}
