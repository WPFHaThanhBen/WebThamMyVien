using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;

namespace WebThamMyVien.Areas.Admin.Controllers
{
    public class ActionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ActionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Area("Admin")]
        public async Task<IActionResult> Index()
        {
            TempData["menu"] = "Action";
            return View();
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetALL()
        {
            // Trong một action method hoặc Action
            List<ActionDto> list = await _unitOfWork.Action.GetAllAction() as List<ActionDto>;

            string json = JsonConvert.SerializeObject(list);

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetActionType(int id)
        {
            // Trong một action method hoặc Action
            ActionTypeDto n = await _unitOfWork.ActionType.GetActionType(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }
        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetUser(int id)
        {
            if (id == 0)
            {
                return Json(new { FullName = "Hệ thống tự động" });
            }
            // Trong một action method hoặc Action
            UserDto n = await _unitOfWork.User.GetUser(id);

            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }
    }
}
