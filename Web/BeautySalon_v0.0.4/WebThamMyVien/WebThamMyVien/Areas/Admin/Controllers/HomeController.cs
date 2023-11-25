using Microsoft.AspNetCore.Mvc;

namespace WebThamMyVien.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            TempData["menu"] = "Home";
            return View();
        }
    }
}
