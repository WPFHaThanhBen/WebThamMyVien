using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;

namespace WebThamMyVien.Areas.Admin.Controllers
{
    public class UserTypeController : Controller
    {
        private readonly IUserTypeRepository _UserTypeRepository;

        public UserTypeController(IUserTypeRepository UserTypeRepository)
        {
            _UserTypeRepository = UserTypeRepository;
        }

        [Area("Admin")]
        public async Task<IActionResult> Index()
        {
            TempData["menu"] = "UserType";
            return View();
        }

        [Area("Admin")]
        public async Task<IActionResult> Create(UserTypeDto? value)
        {
            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "UserType";
            // UserTypeDto hợp lệ
            if (ModelState.IsValid)
            {
                // Add data in DB
                var n = await _UserTypeRepository.CreateUserType(value);
                // Add status is success
                if (n)
                {
                    TempData["success"] = "Thêm mới thông tin thành công";
                    return RedirectToAction("Index");
                }
                // Add status is error
                else
                {
                    TempData["error"] = "Thêm mới thông tin thất bại";
                }
            }
            // UserTypeDto không hợp lệ
            else
            {
                if (value.Name == "" || value.Name == null)
                {
                    return View(value);
                }
                TempData["warning"] = "Thông tin không hợp lệ";
            }
            return View(value);
        }

        [Area("Admin")]
        public async Task<IActionResult> Detail(int? id = 1)
        {
            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "UserType";
            TempData["EditUserType"] = id;
            // Get UserType in DB
            var n = await _UserTypeRepository.GetUserType((int)id);
            return View(n);
        }

        [Area("Admin")]
        public async Task<IActionResult> Edit(UserTypeDto? value)
        {
            // UserTypeDto hợp lệ
            if (ModelState.IsValid)
            {
                // Update data in Db
                var n = await _UserTypeRepository.UpdateUserType(value);
                // Update status is success
                if (n)
                {
                    //Trở về Index và thông báo thành công
                    TempData["success"] = "Cập nhật thông tin thành công";
                    return RedirectToAction("Index");
                }
                // Update status is error
                else
                {
                    // Thông báo thất bại
                    TempData["error"] = "Cập nhật thông tin thất bại";
                }
            }
            // UserTypeDto không hợp lệ
            else
            {
                TempData["warning"] = "Thông tin không hợp lệ";
            }

            return RedirectToAction("Detail", new { id = TempData["EditUserType"] });
        }

        [Area("Admin")]
        public async Task<IActionResult> Delete(int[]? ids)
        {
            //Danh sách id cần xóa không rỗng
            bool dl = false;
            if (ids != null)
            {
                foreach (var id in ids)
                {
                    var n = await _UserTypeRepository.GetUserType(id);
                    dl = await _UserTypeRepository.DeleteUserType(n);

                }
            }
            if (dl)
            {
                TempData["success"] = "Xóa thông tin thành công";
            }
            return RedirectToAction("Index");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetALL()
        {
            // Trong một action method hoặc service
            List<UserTypeDto> list = await _UserTypeRepository.GetAllUserType() as List<UserTypeDto>;

            string json = JsonConvert.SerializeObject(list);

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }
    }
}
