using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;

namespace WebThamMyVien.Areas.Admin.Controllers
{
    public class BranchController : Controller
    {
        private readonly IBranchRepository _BranchRepository;

        public BranchController(IBranchRepository BranchRepository)
        {
            _BranchRepository = BranchRepository;
        }

        [Area("Admin")]
        public async Task<IActionResult> Index()
        {
            TempData["menu"] = "Branch";
            return View();
        }

        [Area("Admin")]
        public async Task<IActionResult> Create(BranchDto? value)
        {
            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "Branch";
            // BranchDto hợp lệ
            if (ModelState.IsValid)
            {
                // Add data in DB
                var n = await _BranchRepository.CreateBranch(value);
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
            // BranchDto không hợp lệ
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
            TempData["menu"] = "Branch";
            TempData["EditBranch"] = id;
            // Get Branch in DB
            var n = await _BranchRepository.GetBranch((int)id);
            return View(n);
        }

        [Area("Admin")]
        public async Task<IActionResult> Edit(BranchDto? value)
        {
            // BranchDto hợp lệ
            if (ModelState.IsValid)
            {
                // Update data in Db
                var n = await _BranchRepository.UpdateBranch(value);
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
            // BranchDto không hợp lệ
            else
            {
                TempData["warning"] = "Thông tin không hợp lệ";
            }

            return RedirectToAction("Detail", new { id = TempData["EditBranch"] });
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
                    var n = await _BranchRepository.GetBranch(id);
                    dl = await _BranchRepository.DeleteBranch(n);

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
            List<BranchDto> list = await _BranchRepository.GetAllBranch() as List<BranchDto>;

            string json = JsonConvert.SerializeObject(list);

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }
    }
}
